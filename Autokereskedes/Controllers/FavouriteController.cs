using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Autokereskedes.Data;
using Autokereskedes.Models;
using Microsoft.AspNetCore.Identity;

namespace Autokereskedes.Controllers
{
    [Authorize]
    public class FavouriteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FavouriteController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToFavourites(int autoId)
        {
            var userId = _userManager.GetUserId(User); // Aktuális felhasználó id-je

            var favouriteExists = await _context.Favourites
                .AnyAsync(f => f.UserId == userId && f.AutoId == autoId);

            if (!favouriteExists)
            {
                var favourite = new Favourite
                {
                    UserId = userId,
                    AutoId = autoId
                };

                _context.Favourites.Add(favourite);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Sikeresen hozzáadtad az autót a kedvencekhez!";
            }

            return RedirectToAction("Index", "Auto");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromFavourites(int autoId)
        {
            var userId = _userManager.GetUserId(User); // Aktuális felhasználó azonosítása

            // A kedvenc autó megkeresése az aktuális felhasználó és az AutoId alapján
            var favourite = await _context.Favourites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.AutoId == autoId);

            if (favourite != null) //törtlés után
            {
                _context.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Sikeresen eltávolítottad az autót a kedvencekből!"; //alertbox megjelenítése
            }

            // Törlés után átirányítás az autók listázásához
            return RedirectToAction("Index", "Favourite");
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var favourites = await _context.Favourites
                .Where(f => f.UserId == userId)
                .Include(f => f.Auto)
                .ToListAsync();

            return View(favourites);
        }
    }
}
