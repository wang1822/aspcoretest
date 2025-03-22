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
        
    }
}