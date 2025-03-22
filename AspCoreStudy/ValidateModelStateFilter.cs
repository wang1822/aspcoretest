using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspCoreStudy
{
    public class ValidateModelStateFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 如果 ModelState 无效，则返回 BadRequest
        if (!context.ModelState.IsValid)
        {
            // 将 ModelState 错误作为 BadRequest 的内容
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
}