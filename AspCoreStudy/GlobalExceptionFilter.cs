using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspCoreStudy
{
    /// <summary>
    /// 一个全局异常过滤器，用于处理异常并显示错误视图。
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        /// <summary>
        /// 构造函数，注入日志记录器。
        /// </summary>
        /// <param name="logger">日志记录器。</param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void OnException(ExceptionContext context)
        {
            // 获取异常信息
            var exception = context.Exception;
            var errorMessage = exception.Message;

            // 记录异常到日志文件
            _logger.LogError(exception, "全局异常捕获：{Message}", errorMessage);

            // 这里我们设置 ViewData["ErrorMessage"]，用于在 View 中弹窗
            var result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(
                    new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                    context.ModelState)
            };

            result.ViewData["ErrorMessage"] = errorMessage;

            context.Result = result;
            context.ExceptionHandled = true; // 标记异常已处理
        }
    }
}