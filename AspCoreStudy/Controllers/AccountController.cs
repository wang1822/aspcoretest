using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string Email, string Password)
    {
        if (Email == "admin@example.com" && Password == "123456") // 模拟登录
        {
            return RedirectToAction("Index", "Home"); // 登录成功跳转
        }

        ViewData["Error"] = "邮箱或密码错误";
        return View();
    }
}