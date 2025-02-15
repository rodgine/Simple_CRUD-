using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly DeveloperService _developerService;

        public DeveloperController(DeveloperService developerService)
        {
            _developerService = developerService;
        }

        // 1. GET all developers (Read)
        public async Task<IActionResult> Index()
        {
            var developers = await _developerService.GetAllDevs();
            return View(developers);
        }

        // 2. GET a single developer by ID
        public async Task<IActionResult> Details(int id)
        {
            var developer = await _developerService.GetDevDetails(id);
            if (developer == null)
            {
                return NotFound();
            }
            return View(developer);
        }

        // 3. GET Create view
        public IActionResult Create()
        {
            return View();
        }

        // 4. POST Create a new developer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Developer developer)
        {
            if (ModelState.IsValid)
            {
                var createdDeveloper = await _developerService.CreateNewDev(developer);
                if (createdDeveloper != null)
                {
                    TempData["SuccessMessage"] = "Developer created successfully!";
                    return RedirectToAction("Index"); // Redirect to Index after successful creation
                }
                ModelState.AddModelError(string.Empty, "Failed to create developer.");
            }
                TempData["SuccessMessage"] = "Developer created successfully!";
                return RedirectToAction("Index"); // Return to Create view with model data if validation fails
        }

        // 5. GET Edit view
        public async Task<IActionResult> Edit(int id)
        {
            var developer = await _developerService.GetDevDetails(id);
            if (developer == null)
            {
                return NotFound(); // If no developer is found, return NotFound
            }
            return View(developer); // Return to the Edit view with the developer data
        }

        // 6. POST Edit an existing developer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Developer developer)
        {
            if (id != developer.Id)
            {
                return NotFound(); // Ensure the ID in the URL matches the ID in the model
            }

            if (ModelState.IsValid)
            {
                var updatedDeveloper = await _developerService.UpdateDev(id, developer);
                if (updatedDeveloper != null)
                {
                    return RedirectToAction("Index"); // Redirect to Index if updated successfully
                }
                ModelState.AddModelError(string.Empty, "Failed to update developer.");
            }
            TempData["SuccessMessage"] = "Developer created successfully!"; // Store error message
            return RedirectToAction("Index"); // ✅ Redirect to Index even if validation fails
        }

        // 7. GET Delete confirmation view
        public async Task<IActionResult> Delete(int id)
        {
            var developer = await _developerService.GetDevDetails(id);
            if (developer == null)
            {
                return NotFound(); // If no developer is found, return NotFound
            }
            TempData["SuccessMessage"] = "Developer created successfully!"; // Store error message
            return RedirectToAction("Index"); // ✅ Redirect to Index even if validation fails
        }

        // 8. POST Delete a developer
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _developerService.DeleteDev(id); // Call the DeleteDev method in the service
            if (result)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index if the developer is successfully deleted
            }
            TempData["SuccessMessage"] = "Developer created successfully!"; // Store error message
            return RedirectToAction("Index"); // ✅ Redirect to Index even if validation fails
        }
    }
}
