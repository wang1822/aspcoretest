using AspCoreStudy.Models;
using AspCoreStudy.Repositories;
using Microsoft.AspNetCore.Mvc;

//实现restful风格的api
// [Route("api/auth")]
// [ApiController]
public class AccountController : Controller
{
    //注入用户仓储
    private readonly IUserRepository _userRepository;

    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username, user.Password);

         if (existingUser != null)
         {
             return RedirectToAction("Index", "Home");
         }
        
        return View();
    }
}