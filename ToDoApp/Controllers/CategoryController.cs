using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CategoryController : Controller
{
    public static List<Category> categories = new List<Category>();

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
