using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Arithmetic;
using CommerceExchanger.Core.Exceptions;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services.Implementations
{
    public class BaseCurrencyExchangeRateProvider : IExchangeRateProvider
    {
        private readonly RoundCalculator _roundCalculator;
        private readonly IExchangeRateStorage _exchangeRateStorage;
        private readonly Currency _baseCurrency;

        public BaseCurrencyExchangeRateProvider(Currency baseCurrency,
            IExchangeRateStorage exchangeRateStorage, RoundCalculator roundCalculator)
        {
            _exchangeRateStorage = exchangeRateStorage;
            _baseCurrency = baseCurrency;
            _roundCalculator = roundCalculator;
        }

        public async Task<ExchangeResult> GetExchangeRate(ExchangeRateRequest request)
        {
            var fromRate = await GetExchangeRateToBaseCurrency(request.From);
            var toRate = await GetExchangeRateToBaseCurrency(request.To);
            return new ExchangeResult(request.To, _roundCalculator.EnsureRate(fromRate / toRate));
        }

        protected virtual async Task<decimal> GetExchangeRateToBaseCurrency(Currency currency)
        {
            if (await _exchangeRateStorage.Get(new ExchangeRateRequest(currency, _baseCurrency), out var rate))
            {
                return rate;
            }

            throw new CurrencyExchangeException(currency);
        }

        public Task<IEnumerable<Currency>> GetAvailableCurrencies()
        {
            return _exchangeRateStorage.GetCurrencies();
        }
    }
}