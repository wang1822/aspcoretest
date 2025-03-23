using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspCoreStudy.Models;
using AspCoreStudy.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreStudy.Controllers
{

    /// <summary>
    /// 后台 API 操作的控制器。  
    /// </summary>  
    /// <param name="userService">用于用户相关操作的服务。</param>  
    [ApiController]
    [Route("api/v1/[controller]")]
    [ValidateModelStateFilter]
    public class BackStageApiController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;


        /// <summary>
        /// 从系统中查询所有用户。
        /// </summary>
        /// <returns>包含用户列表的 IActionResult。</returns>
        [HttpGet("users")]
        [Authorize(Policy = "ViewPage")]
        public async Task<IActionResult> QueryUsers()
        {
            List<User> users = await _userService.FetchAllUsersAsync();

            return Ok(users);
        }
    }
}