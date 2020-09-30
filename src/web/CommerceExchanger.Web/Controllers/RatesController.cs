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
        public async Task<ExchangeResult> Index(string from, string to)
        {
            return await _exchangeRateProvider.GetExchangeRateAsync(new ExchangeRateRequest(Currency.GetCurrency(from),
                Currency.GetCurrency(to)));
        }

        [HttpGet]
        public async Task<IEnumerable<ExchangeResult>> ByCurrency(string currency)
        {
            var sourceCurrency = Currency.GetCurrency(currency);
            var allCurrencies = (await _exchangeRateProvider.GetAvailableCurrenciesAsync()).ToList();
            return await ExchangeRateForAllCurrencies(sourceCurrency,
                allCurrencies.ExceptSingle(sourceCurrency, Currency.DefaultComparer));
        }

        private IEnumerable<Task<ExchangeResult>> ExchangeRateForAllCurrencies(Currency from,
            IEnumerable<Currency> currencies)
        {
            foreach (var currency in currencies)
            {
                yield return _exchangeRateProvider.GetExchangeRateAsync(new ExchangeRateRequest(@from, currency));
            }
        }
    }
}