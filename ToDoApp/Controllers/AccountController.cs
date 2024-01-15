using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoApp.Models;
public class AccountController : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Sprawdź poprawność danych logowania
            if (model.Username == "admin" && model.Password == "adminpassword")
            {
                await AuthenticateUser("Admin");
                TempData["name"] = model.Username;
                return RedirectToAction("Hello", "Account");
            }
            else if (model.Username == "user" && model.Password == "userpassword")
            {
                await AuthenticateUser("User");
                TempData["name"] = model.Username;
                return RedirectToAction("Hello", "Account");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
        }

        return View(model);
    }

    private async Task AuthenticateUser(string role)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, role),
        new Claim(ClaimTypes.Role, role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            // Ustawienia właściwości autentykacji
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                      new ClaimsPrincipal(claimsIdentity),
                                      authProperties);
    }


    [Authorize]
    public IActionResult AdminAction()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
    public IActionResult AccessDenied()
    {
        return View();
    }

    [Authorize]
    public IActionResult Hello()
    {
        string name = TempData["name"] as string;
        ViewBag.name = name;
        return View();
    }
}
