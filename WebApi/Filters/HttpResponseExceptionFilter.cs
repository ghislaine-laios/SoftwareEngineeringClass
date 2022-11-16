using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Exceptions;

namespace WebApi.Filters
{
    /**
     * <summary>处理自定义HTTP异常的异步Filter。</summary>
     */
    public class HttpResponseExceptionFilter : IAsyncActionFilter
    {
        /**
         * <summary>处理自定义HTTP异常。</summary>
         */
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionExecutedContext = await next();
            // Only handle our exceptions.
            var exception = actionExecutedContext.Exception;
            if (exception is not HttpException httpException) return;
            actionExecutedContext.Result = new JsonResult(new { code = httpException.StatusCode, message = httpException.Message }) { StatusCode = httpException.StatusCode };
            actionExecutedContext.ExceptionHandled = true;
        }
    }
}
