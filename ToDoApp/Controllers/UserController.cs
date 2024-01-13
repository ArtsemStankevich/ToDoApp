using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Controllers
{
    public class UserController : Controller
    {
        public static List<User> _users = new List<User>();

        [Authorize]
        public IActionResult Index()
        {
            var users = _users; // Tutaj przypisz rzeczywiste dane
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (TryValidateModel(user, nameof(user)))
            {
                _users.Add(user);
                return RedirectToAction("Index", "User");
            }
            else
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    var errorMessage = modelError.ErrorMessage;
                    Console.WriteLine(errorMessage);
                }
            return View(user);
        }
    }

}
