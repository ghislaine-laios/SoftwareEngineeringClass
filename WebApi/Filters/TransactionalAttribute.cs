using System.Diagnostics;
using System.Transactions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters
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
            var options = new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead };
            using var transactionScope = new TransactionScope(TransactionScopeOption.Required, options,
                TransactionScopeAsyncFlowOption.Enabled);
            var actionExecutedContext = await next();
            //if no exception were thrown
            if (actionExecutedContext.Exception == null)
                transactionScope.Complete();
        }
#pragma warning restore CS1591
    }
}
