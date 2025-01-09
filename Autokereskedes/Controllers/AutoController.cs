using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Autokereskedes.Data;
using Autokereskedes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Autokereskedes.Controllers
{
    public class AutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> MyAds(Auto auto)
        {
            var userId = User.Identity.Name;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var autoList = await _context.Autos
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return View(autoList);
        }
        // Index: Az autók listázása szűrési lehetőséggel
        public async Task<IActionResult> Index(Brands? brand, int? yearFrom, int? yearTo, int? capacityFrom, int? capacityTo, decimal? priceFrom, decimal? priceTo, BodyType? bodyType, FuelType? fuelType)
        {
            ViewBag.Brands = new SelectList(Enum.GetValues(typeof(Brands)).Cast<Brands>());
            ViewBag.Years = new SelectList(Enumerable.Range(1970, DateTime.Now.Year - 1969));
            ViewBag.FuelTypes = new SelectList(Enum.GetValues(typeof(FuelType)).Cast<FuelType>());
            ViewBag.Capacities = new SelectList(await _context.Autos.Select(a => a.Capacity).Distinct().ToListAsync());
            ViewBag.BodyTypes = new SelectList(Enum.GetValues(typeof(BodyType)).Cast<BodyType>());

            // auto tábla lekérdezése
            var autos = _context.Autos.AsQueryable();

            // Szűrés részleg, a felhasználó által kiválasztott szűrési feltételek alapján
            if (brand.HasValue)
            {
                autos = autos.Where(a => a.Brand == brand);
            }

            if (yearFrom.HasValue)
            {
                autos = autos.Where(a => a.Year >= yearFrom);
            }

            if (yearTo.HasValue)
            {
                autos = autos.Where(a => a.Year <= yearTo);
            }

            if (fuelType.HasValue)
            {
                autos = autos.Where(a => a.Fuel_Type == fuelType);
            }

            if (capacityFrom.HasValue)
            {
                autos = autos.Where(a => a.Capacity >= capacityFrom);
            }

            if (capacityTo.HasValue)
            {
                autos = autos.Where(a => a.Capacity <= capacityTo);
            }

            if (priceFrom.HasValue)
            {
                autos = autos.Where(a => a.Price >= priceFrom);
            }

            if (priceTo.HasValue)
            {
                autos = autos.Where(a => a.Price <= priceTo);
            }

            if (bodyType.HasValue)
            {
                autos = autos.Where(a => a.Body_Type == bodyType);
            }

            var autoList = await autos.ToListAsync();

            return View(autoList);
        }

        // Az új autó hozzáadása itt történik
        [Authorize]
        public IActionResult Create()
        {
            // A select list-eket itt töltjük be, a már meglévő adatokkal
            ViewBag.Brands = new SelectList(Enum.GetValues(typeof(Brands)).Cast<Brands>());
            ViewBag.Years = new SelectList(Enumerable.Range(1970, DateTime.Now.Year - 1969));
            ViewBag.FuelTypes = new SelectList(Enum.GetValues(typeof(FuelType)).Cast<FuelType>());
            ViewBag.BodyTypes = new SelectList(Enum.GetValues(typeof(BodyType)).Cast<BodyType>());
            ViewBag.Capacities = new SelectList(_context.Autos.Select(a => a.Capacity).Distinct());

            return View();
        }

        [HttpPost] //csak http post metodusokkal hivhato meg
        [ValidateAntiForgeryToken] //védelmet nyújt, ellenőrzi, hogy a beküldött kérés tartalmaz-e érvényes anti-forgery token-t, amit az ASP generál az űrlaphoz.

        [Authorize]
        public async Task<IActionResult> Create(Auto auto)
        {
            if (ModelState.IsValid)
            {
                auto.UserId = User.Identity.Name;
                _context.Add(auto);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Sikeresen létrehoztad a hírdetést!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = new SelectList(Enum.GetValues(typeof(Brands)).Cast<Brands>());
            ViewBag.Years = new SelectList(Enumerable.Range(1970, DateTime.Now.Year - 1969));
            ViewBag.FuelTypes = new SelectList(Enum.GetValues(typeof(FuelType)).Cast<FuelType>());
            ViewBag.BodyTypes = new SelectList(Enum.GetValues(typeof(BodyType)).Cast<BodyType>());
            ViewBag.Capacities = new SelectList(_context.Autos.Select(a => a.Capacity).Distinct());

            return View(auto);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FindAsync(id);
            if (auto == null)
            {
                return NotFound();
            }

            ViewBag.FuelTypes = new SelectList(Enum.GetValues(typeof(FuelType)).Cast<FuelType>(), auto.Fuel_Type);
            ViewBag.BodyTypes = new SelectList(Enum.GetValues(typeof(BodyType)).Cast<BodyType>(), auto.Body_Type);

            return View(auto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,Color,Year,Fuel_Type,Body_Type,Condition,Extras,Price,Capacity,Description,ImgUrl")] Auto auto)
        {
            if (id != auto.Id)
                return NotFound();

            var currentUserId = User.Identity.Name;
            if (currentUserId == null)
                return RedirectToAction("Login", "Account");

            var existingAuto = await _context.Autos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id); //AsNoTracking() -> kikapcsolja az EntityFramework követési mechanizmusát, ezzel a fugvennyel csak olvassuk az adatot 
            if (existingAuto == null || existingAuto.UserId != currentUserId)                            //FirstOrDefaultAsync() -> az első olyan autót keresi ami megfelel a feltételeknek, jelenesetben az id megegyezik, ha talál ilyen autót visszaadja azt, ha nem akkor null-t ad vissza.
                return Forbid();

            if (ModelState.IsValid) //ellenőrzés, hogy a bevitt adatok megfelelnek-e a modell struktúrájához
            {
                try
                {
                    auto.UserId = currentUserId;
                    _context.Update(auto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) //ha két felhasználó egyszerre módosítana
                {
                    if (!AutoExists(auto.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(auto);
        }

        // Autó törlés itt zajlik
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var auto = await _context.Autos.FirstOrDefaultAsync(a => a.Id == id);
            if (auto == null || auto.UserId != User.Identity.Name)
                return Forbid();

            return View(auto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var auto = await _context.Autos.FindAsync(id);
            if (auto == null || auto.UserId != User.Identity.Name)
            {
                return Forbid();
            }

            _context.Autos.Remove(auto); //töröljük az adott autot id alapján
            await _context.SaveChangesAsync(); //elmentjük az adatbázist
            return RedirectToAction(nameof(Index));
        }

        // Összes autó törlése, csak admin jogosultsággal rendelkezők tudják használni.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAll()
        {
            var autos = _context.Autos.ToList();
            if (autos.Any())
            {
                _context.Autos.RemoveRange(autos);
                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Az összes hirdetés sikeresen törölve lett.";
            return RedirectToAction(nameof(Index));
        }

        //Az autók részletes nézete az autó adataival, és a feltöltő felhasználó profiladataival.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FirstOrDefaultAsync(a => a.Id == id);

            if (auto == null)
            {
                return NotFound();
            }

            var sellerProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == auto.UserId);

            ViewBag.SellerProfile = sellerProfile;

            return View(auto);
        }

        private bool AutoExists(int id)
        {
            return _context.Autos.Any(e => e.Id == id);
        }
    }
}
