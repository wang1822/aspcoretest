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

            // 创建一个包含错误信息的响应对象
            var errorResponse = new
            {
                Success = false,
                ErrorMessage = errorMessage
            };

            // 设置返回的 JSON 格式的响应
            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = 500  // 你可以根据具体的异常类型设置不同的状态码
            };
            context.ExceptionHandled = true; // 标记异常已处理
        }
    }
}