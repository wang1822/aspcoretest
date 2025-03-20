using AspCoreStudy.Models;
using AspCoreStudy.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Controllers
{
    /// <summary>
    /// 用于管理用户账户的控制器，包括登录和注册。
    /// </summary>
    /// <param name="userService">Service for user-related operations.</param>
    /// <param name="roleService">Service for role-related operations.</param>
    /// <param name="tokenService">Service for token generation and management.</param>
    public class AccountController(IUserService userService, IRoleService roleService
    , TokenService tokenService) : Controller
    {
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;
        private readonly TokenService _tokenService = tokenService;

        /// <summary>
        /// 显示登录页面。
        /// </summary>
        /// <returns>The login view.</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        
        /// <summary>
        /// 显示注册页面。
        /// </summary>
        /// <returns>The registration view.</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 通过创建新用户并分配默认角色来处理用户注册。
        /// </summary>
        /// <param name="user">The user object containing registration details.</param>
        /// <returns>A view for the registration page or a redirect to the login page upon success.</returns>
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            //检查用户名是否已存在
            await _userService.FetchUserByUsernameAsync(user.Username);

            // 创建新用户
            await _userService.CreateAsync(user);

            // 获取 user 角色
            Role userRole = await _roleService.FetchRoleByNameAsync("user");

            // 赋予 user 角色
            await _userService.AssignRoleToUserAsync(user.Id, userRole.Id);

            return RedirectToAction("Login", "Account");
        }

        //登录
        /// <summary>
        /// 通过验证凭据并生成令牌来处理用户登录。
        /// </summary>
        /// <param name="user">The user object containing login details.</param>
        /// <returns>A view for the login page or a redirect to the home page upon success.</returns>
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            User existingUser = await _userService.AuthenticateUserAsync(user.Username, user.PasswordHash);

            // if (existingUser == null)
            // {
            //     // Add error message for incorrect username or password
            //     ModelState.AddModelError("Username", "用户名或密码错误");
            //     return View(user);
            // }

            // 获取用户权限
            List<string> permissions = await _userService.FetchPermissionsForUserAsync(existingUser.Id);
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
}