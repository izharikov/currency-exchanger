using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CommerceExchanger.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommerceExchanger.Web.Filters
{
    public class CurrencyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is CurrencyExchangeException currencyExchangeException)
            {
                var result = JsonSerializer.Serialize(currencyExchangeException.ToJson());
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
                await context.HttpContext.Response.WriteAsync(result);
            }

            await base.OnExceptionAsync(context);
        }
    }
}