using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Controllers;

public class CategoryController : Controller
{
    public static List<Category> categories = new List<Category>();
    public CategoryController()
    {
        if (!categories.Any(c => c.Name == "Unknown category"))
        {
            var unknownCategory = new Category { Name = "Unknown category" };
            categories.Add(unknownCategory);
        }
    }

    [Authorize]
    public IActionResult Index()
    {
        var categoriees = categories; // Tutaj przypisz rzeczywiste dane
        return View(categoriees);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (TryValidateModel(category, nameof(category)))
        {
            categories.Add(category);
            return RedirectToAction("Index", "Category");
        }
        else
            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMessage = modelError.ErrorMessage;
                Console.WriteLine(errorMessage);
            }
        return View(category);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        Category category = categories.Where(e => e.Id == id).FirstOrDefault();
        return View(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Edit(Category category, int id)
    {
        if (ModelState.IsValid)
        {
            categories.Where(e => e.Id == id).FirstOrDefault().Name = category.Name;
        }
        return RedirectToAction("Index");
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        Category category = categories.Where(e => e.Id == id).FirstOrDefault();
        if (category != null)
        {
            // Aktualizuj zadania związane z usuwaną kategorią na null
            foreach (var task in TaskController._tasks.Where(t => t.CategoryId == id))
            {
                task.CategoryId = 1;
            }

            // Usuń kategorię
            categories.Remove(category);
        }
        return RedirectToAction("Index");
    }
}
