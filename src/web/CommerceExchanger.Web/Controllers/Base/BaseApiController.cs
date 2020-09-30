using CommerceExchanger.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CommerceExchanger.Web.Controllers.Base
{
    [ApiController]
    [Route("[controller]/{action=Index}")]
    [CurrencyExceptionFilter]
    public abstract class BaseApiController : ControllerBase
    {
    }
}