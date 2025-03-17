using AspCoreStudy.Models;
using AspCoreStudy.Repositories;
using AspCoreStudy.Services;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    //注入用户仓储
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly TokenService _tokenService;

    public AccountController(IUserRepository userRepository, IRoleRepository roleRepository
    , TokenService tokenService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tokenService = tokenService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    //注册
    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        //检查用户名是否已存在
        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "用户名已存在");
            return View(user);
        }

        // 创建新用户
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // 使用 BCrypt 哈希密码
        user.CreatedAt = DateTime.Now;
        await _userRepository.CreateAsync(user);

        // 获取 user 角色
        var userRole = await _roleRepository.GetRoleByNameAsync("user");

        // 赋予 user 角色
        await _userRepository.AssignRoleAsync(user.Id, userRole.Id);

        return RedirectToAction("Login", "Account");
    }

    //登录
    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username, user.PasswordHash);

        if (existingUser == null)
        {
            // Add error message for incorrect username or password
            ModelState.AddModelError("Username", "用户名或密码错误");
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