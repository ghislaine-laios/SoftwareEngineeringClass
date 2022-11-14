using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Transactions;

namespace WebApiPlayground.Filters
{
    /**
     * <summary>指示Action需要被事务包围。</summary>
     */
    public class TransactionalAttribute : Attribute, IAsyncActionFilter
    {
#pragma warning disable CS1591
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Trace.WriteLine("In transaction.");
            using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var actionExecutedContext = await next();
            //if no exception were thrown
            if (actionExecutedContext.Exception == null)
                transactionScope.Complete();
        }
#pragma warning restore CS1591
    }
}
