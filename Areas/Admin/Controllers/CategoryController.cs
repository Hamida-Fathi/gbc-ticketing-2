using GBC_TicketingSystem_2.Areas.Admin.Models;
using GBC_TicketingSystem_2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GBC_TicketingSystem_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ApplicationDbContext context, ILogger<CategoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

     
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Admin opened Category list page.");
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

   
        public IActionResult Create()
        {
            _logger.LogInformation("Admin opened Create Category page.");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Category creation failed validation.");
                return View(category);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category '{CategoryName}' created successfully.", category.Name);
            TempData["success"] = "Category created successfully!";

            return RedirectToAction(nameof(Index));
        }

   
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                _logger.LogError("CategoryId {CategoryId} not found for editing.", id);
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogError("CategoryId mismatch during edit. URL ID {UrlId}, Model ID {ModelId}.", id, category.CategoryId);
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category '{CategoryName}' (ID {CategoryId}) updated successfully.",
                                   category.Name, id);
            TempData["success"] = "Category updated successfully!";

            return RedirectToAction(nameof(Index));
        }

   
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                _logger.LogError("CategoryId {CategoryId} not found for delete.", id);
                return NotFound();
            }

            return View(category);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                _logger.LogError("CategoryId {CategoryId} not found during delete action.", id);
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Category '{CategoryName}' (ID {CategoryId}) deleted successfully.",
                                   category.Name, id);

            TempData["success"] = "Category deleted successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
