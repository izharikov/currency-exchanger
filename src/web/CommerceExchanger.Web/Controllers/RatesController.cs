using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Utils.Extensions;
using CommerceExchanger.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CommerceExchanger.Web.Controllers
{
    public class RatesController : BaseApiController
    {
        private readonly IExchangeRateProvider _exchangeRateProvider;

        public RatesController(IExchangeRateProvider exchangeRateProvider)
        {
            _exchangeRateProvider = exchangeRateProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<Currency>> All()
        {
            return await _exchangeRateProvider.GetAvailableCurrencies();
        }

        [HttpGet]
        public async Task<ExchangeResult> Index(string from, string to)
        {
            return await _exchangeRateProvider.GetExchangeRate(new ExchangeRateRequest()
                {
                    From = new Currency(from),
                    To = new Currency(to)
                }
            );
        }

        [HttpGet]
        public async Task<IEnumerable<ExchangeResult>> Currency(string currency)
        {
            var sourceCurrency = new Currency(currency);
            var allCurrencies = await _exchangeRateProvider.GetAvailableCurrencies();
            return await allCurrencies.Except(sourceCurrency)
                .Select(c => _exchangeRateProvider.GetExchangeRate(new ExchangeRateRequest()
                {
                    From = sourceCurrency,
                    To = c
                }));
        }
    }
}