using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Autokereskedes.Data;
using Autokereskedes.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

[Authorize]
public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProfileController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Index: Profil megtekintése
    public async Task<IActionResult> Index()
    {
        var userId = User.Identity.Name; // Aktuális felhasználó azonosítója
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile == null)
        {
            return RedirectToAction("Create");
        }

        return View(profile);
    }

    // Profil űrlap megjelenítése (GET)
    public IActionResult Create()
    {
        return View();
    }

    // Profil létrehozása (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Profile profile)
    {
        if (ModelState.IsValid)
        {
            // Automatikusan beállítjuk a UserId-t a bejelentkezett felhasználó azonosítójával
            profile.UserId = User.Identity.Name; // Vagy _userManager.GetUserId(User)

            _context.Add(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(profile);
    }

    // Profil szerkesztése (GET)
    public async Task<IActionResult> Edit()
    {
        var userId = User.Identity.Name;
        var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile == null)
        {
            return RedirectToAction("Create");
        }

        return View(profile);
    }

    // Profil szerkesztése (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Profile profile)
    {
        if (ModelState.IsValid)
        {
            // Ellenőrizzük, hogy a UserId nem változott-e
            var existingProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == User.Identity.Name);
            if (existingProfile == null)
            {
                return NotFound();
            }

            existingProfile.Name = profile.Name;
            existingProfile.PhoneNumber = profile.PhoneNumber;
            existingProfile.Address = profile.Address;
            existingProfile.BirthDate = profile.BirthDate;
            existingProfile.ProfilePictureUrl = profile.ProfilePictureUrl;

            _context.Update(existingProfile);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(profile);
    }

}
