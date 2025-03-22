using AspCoreStudy.Models;
using AspCoreStudy.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ValidateModelStateFilter] 
    public class AccountApiController(IUserService userService, IRoleService roleService, TokenService tokenService) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IRoleService _roleService = roleService;
        private readonly TokenService _tokenService = tokenService;
        /// <summary>
        /// 通过创建新用户并分配默认角色来处理用户注册。
        /// </summary>
        /// <param name="user">The user object containing registration details.</param>
        /// <returns>A response indicating the success or failure of the registration.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            //检查用户名是否已存在
            await _userService.FetchUserByUsernameAsync(user.Username);

            // 获取 user 角色
            Role userRole = await _roleService.FetchRoleByNameAsync("User");

            // 创建新用户
            await _userService.CreateAsync(user);

            // 赋予 user 角色
            await _userService.AssignRoleToUserAsync(user.Id, userRole.Id);

            return Ok();
        }

        /// <summary>
        /// 通过验证凭据并生成令牌来处理用户登录。
        /// </summary>
        /// <param name="user">The user object containing login details.</param>
        /// <returns>A response containing a token if login is successful.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            // 验证用户
            User existingUser = await _userService.AuthenticateUserAsync(user.Username, user.PasswordHash);

            // 获取用户权限
            List<string> permissions = await _userService.FetchPermissionsForUserAsync(existingUser.Id);

            // 生成 Token
            var token = _tokenService.GenerateToken(user.Username, permissions);

            // 返回 Token
            return Ok(new { Token = token });
        }
    }
}