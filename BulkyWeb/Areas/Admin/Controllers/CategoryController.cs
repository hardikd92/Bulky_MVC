using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.Cateogry.GetAll().ToList();
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
                _unitOfWork.Cateogry.Add(obj);
                _unitOfWork.Save();
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
            Category selectedCategory = _unitOfWork.Cateogry.Get(u => u.Id == id);
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
                _unitOfWork.Cateogry.Update(obj);
                _unitOfWork.Save();
                TempData["SucessMsg"] = "Category Updated Sucessfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            Category category = _unitOfWork.Cateogry.Get(i => i.Id == id);
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
            Category category = _unitOfWork.Cateogry.Get(i => i.Id == id);
            if (category == null)
            {
                NotFound();
            }
            _unitOfWork.Cateogry.Remove(category);
            _unitOfWork.Save();
            TempData["SucessMsg"] = "Category Deleted Sucessfully.";
            return RedirectToAction("Index");
        }
    }
}
