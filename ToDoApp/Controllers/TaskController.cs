using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class TaskController : Controller
{
    private static List<TaskItem> _tasks = new List<TaskItem>();
    private static List<Category> _categories = new List<Category>();

    public IActionResult Index()
    {
        var tasks = _tasks; // Tutaj przypisz rzeczywiste dane
        return View(tasks);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();
        Console.WriteLine(ViewBag.Categories);
        return View();
    }

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        if (ModelState.IsValid)
        {
            _tasks.Add(task);
            return RedirectToAction("Index", "Task");
        }
        else
            foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
            {
                var errorMessage = modelError.ErrorMessage;
                Console.WriteLine(errorMessage);
            }



        return View(task);
    }
}
