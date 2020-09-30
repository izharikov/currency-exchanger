using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Utils.Extensions;
using CommerceExchanger.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CommerceExchanger.Web.Controllers
{
    public class RateProviderController : BaseApiController
    {
        private readonly IEnumerable<IExternalRateProvider> _rateProviders;

        public RateProviderController(IEnumerable<IExternalRateProvider> rateProviders)
        {
            _rateProviders = rateProviders;
        }

        [HttpGet]
        public async Task<object> RequestProvider(string name, string date)
        {
            var rateProvider = _rateProviders.FirstOrDefault(x => x.Name == name);
            return rateProvider != null
                ? await rateProvider.Request(DateTimeExtensions.Parse(date, "yyyy-MM-dd", out var res)
                    ? res
                    : DateTimeOffset.Now)
                : (object) NotFound($"Provider '{name}' not found.");
        }
    }
}