using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = _db.Categories.ToList<Category>();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Category Name and Display Order cannot exactly same.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["SucessMsg"] = "Category Added Sucessfully.";
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category selectedCategory = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (selectedCategory == null)
            {
                return NotFound();
            }
            return View(selectedCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["SucessMsg"] = "Category Updated Sucessfully.";
                return RedirectToAction("Index");
            }
            return View() ;
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            { 
                NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(i=>i.Id == id);
            if (category == null)
            {
                NotFound();                
            }
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            Category category = _db.Categories.FirstOrDefault(i => i.Id == id);
            if (category == null)
            {
                NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["SucessMsg"] = "Category Deleted Sucessfully.";
            return RedirectToAction("Index");
        }
    }
}
