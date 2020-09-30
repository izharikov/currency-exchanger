using System.Collections.Generic;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Web.Controllers.Base;

namespace CommerceExchanger.Web.Controllers
{
    public class CurrenciesController : BaseApiController
    {
        public IEnumerable<Currency> All()
        {
            return Currency.GetAll();
        }
    }
}