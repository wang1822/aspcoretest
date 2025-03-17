using AspCoreStudy.Models;
using AspCoreStudy.Services;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    //注入用户仓储
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly TokenService _tokenService;

    public AccountController(IUserService userService, IRoleService roleService, TokenService tokenService)
    {
        _userService = userService;
        _roleService = roleService;
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
        var existingUser = await _userService.FetchUserByUsernameAsync(user.Username);
        if (existingUser != null)
        {
            ModelState.AddModelError("Username", "用户名已存在");
            return View(user);
        }

        // 创建新用户
        await _userService.CreateAsync(user);

        // 获取 user 角色
        var userRole = await _roleService.FetchRoleByNameAsync("user");

        // 赋予 user 角色
        await _userService.AssignRoleToUserAsync(user.Id, userRole.Id);

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

        var existingUser = await _userService.AuthenticateUserAsync(user.Username, user.PasswordHash);

        if (existingUser == null)
        {
            // Add error message for incorrect username or password
            ModelState.AddModelError("Username", "用户名或密码错误");
            return View(user);
        }

        // 获取用户权限
        var permissions = await _userService.FetchPermissionsForUserAsync(existingUser.Id);

        //生成token
        var token = _tokenService.GenerateToken(user.Username, permissions);

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