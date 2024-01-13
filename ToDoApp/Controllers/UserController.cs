using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace ToDoApp.Controllers
{
    public class UserController : Controller
    {
        public static List<User> _users = new List<User>();
        public UserController()
        {
            if (!_users.Any(c => c.Username == "Unknown username"))
            {
                var unknownUsername = new User { Username = "Unknown username" };
                _users.Add(unknownUsername);
            }
        }

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
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            User user = _users.Where(e => e.Id == id).FirstOrDefault();
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(User user, int id)
        {
            if (ModelState.IsValid)
            {
                _users.Where(e => e.Id == id).FirstOrDefault().Username = user.Username;
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            User user = _users.Where(e => e.Id == id).FirstOrDefault();
            if (user != null)
            {
                // Aktualizuj zadania związane z usuwaną kategorią na null
                foreach (var task in TaskController._tasks.Where(t => t.UserId == id))
                {
                    task.UserId = 1;
                }

                // Usuń kategorię
                _users.Remove(user);
            }
            return RedirectToAction("Index");
        }
    }



}
