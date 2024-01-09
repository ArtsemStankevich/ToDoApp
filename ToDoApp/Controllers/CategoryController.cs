using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CategoryController : Controller
{
    private static List<Category> _categories = new List<Category>();

    public IActionResult Index()
    {
        var categories = _categories; // Tutaj przypisz rzeczywiste dane
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (TryValidateModel(category, nameof(category)))
        {
            _categories.Add(category);
            return RedirectToAction("Index", "Task");
        }
        else
            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMessage = modelError.ErrorMessage;
                Console.WriteLine(errorMessage);
            }
        return View(category);
    }
}
