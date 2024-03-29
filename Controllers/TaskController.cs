﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.Controllers;

public class TaskController : Controller
{
    public static List<TaskItem> _tasks = new List<TaskItem>();

    [Authorize]
    public IActionResult Index()
    {
        var tasks = _tasks;
        var categories = CategoryController.categories.ToDictionary(c => c.Id, c => c.Name);
        var users = UserController._users.ToDictionary(c => c.Id, c => c.Username);
        ViewBag.CategoryNames = categories;
        ViewBag.Usernames = users;
        return View(tasks);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(CategoryController.categories, "Id", "Name");
        ViewBag.Users = new SelectList(UserController._users, "Id", "Username");
        Console.WriteLine(CategoryController.categories);
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        ViewBag.Categories = new SelectList(CategoryController.categories, "Id", "Name");
        ViewBag.Users = new SelectList(UserController._users, "Id", "Username");
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

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        TaskItem task = _tasks.Where(e => e.Id == id).FirstOrDefault();
        ViewBag.Categories = new SelectList(CategoryController.categories, "Id", "Name");
        ViewBag.Users = new SelectList(UserController._users, "Id", "Username");
        ViewBag.DoneStatusOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Done" },
            new SelectListItem { Value = "false", Text = "Not done" }
        };
        return View(task);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Edit(TaskItem task, int id)
    {
        ViewBag.Categories = new SelectList(CategoryController.categories, "Id", "Name");
        ViewBag.Users = new SelectList(UserController._users, "Id", "Username");
        ViewBag.DoneStatusOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "true", Text = "Done" },
            new SelectListItem { Value = "false", Text = "Not done" }
        };
        if (ModelState.IsValid)
        {
            _tasks.Where(e => e.Id == id).FirstOrDefault().Done = task.Done;
            _tasks.Where(e => e.Id == id).FirstOrDefault().Description = task.Description;
            _tasks.Where(e => e.Id == id).FirstOrDefault().DueDate = task.DueDate;
            _tasks.Where(e => e.Id == id).FirstOrDefault().CategoryId = task.CategoryId;
            _tasks.Where(e => e.Id == id).FirstOrDefault().UserId = task.UserId;
            return RedirectToAction("Index");
        }
        return View(task);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Delete(int id)
    {
        TaskItem task = _tasks.Where(e => e.Id == id).FirstOrDefault();
        _tasks.Remove(task);
        return RedirectToAction("Index");
    }
}
