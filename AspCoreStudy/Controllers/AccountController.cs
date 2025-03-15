using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string UserName, string Password)
    {
        if (UserName == "admin@example.com" && Password == "123456")
        {
            return RedirectToAction("Index", "Home");
        }

        ViewData["Error"] = "邮箱或密码错误";
        return View();
    }
}