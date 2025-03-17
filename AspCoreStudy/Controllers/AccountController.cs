using AspCoreStudy.Models;
using AspCoreStudy.Repositories;
using AspCoreStudy.Services;
using Microsoft.AspNetCore.Mvc;

//实现restful风格的api
// [Route("api/auth")]
// [ApiController]
public class AccountController : Controller
{
    //注入用户仓储
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public AccountController(IUserRepository userRepository, TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
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

        if (existingUser == null)
        {
            // Add error message for incorrect username or password
            ModelState.AddModelError(string.Empty, "用户名或密码错误");
            return View(user);
        }

        //生成token
        var token = _tokenService.GenerateToken(user.Username);

        // 将 Token 保存到 Cookie 中
        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });


        return RedirectToAction("Index", "Home");
    }
}