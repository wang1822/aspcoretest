using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspCoreStudy.Controllers
{

    /// <summary>
    /// 用于管理后台操作的控制器。
    /// </summary>
    [Route("[controller]")]
    public class BackStageController : Controller
    {
        /// <summary>
        /// 显示用户管理视图。
        /// </summary>
        /// <returns>用户管理视图。</returns>
        public IActionResult Usermanagement()
        {
            return View();
        }
    }
}