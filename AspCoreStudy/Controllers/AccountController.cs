using AspCoreStudy.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }
        
        return View();
    }
}