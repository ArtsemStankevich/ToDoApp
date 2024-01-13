using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

public class TaskController : Controller
{
    private static List<TaskItem> _tasks = new List<TaskItem>();

    [Authorize]
    public IActionResult Index()
    {
        var tasks = _tasks;
        var categories = CategoryController.categories.ToDictionary(c => c.Id, c => c.Name);
        ViewBag.CategoryNames = categories;
        return View(tasks);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(CategoryController.categories, "Id", "Name");
        Console.WriteLine(CategoryController.categories);
        return View();
    }

    [Authorize(Roles = "Admin")]
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
