using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Arithmetic;
using CommerceExchanger.Core.Exceptions;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services.Implementations
{
    public class BaseCurrencyExchangeRateProvider : IExchangeRateProvider
    {
        private readonly Currency _baseCurrency;
        private readonly ExchangeRetriever _exchangeRetriever;
        private readonly RoundCalculator _roundCalculator;

        public BaseCurrencyExchangeRateProvider(Currency baseCurrency,
            ExchangeRetriever exchangeRetriever, RoundCalculator roundCalculator)
        {
            _exchangeRetriever = exchangeRetriever;
            _baseCurrency = baseCurrency;
            _roundCalculator = roundCalculator;
        }

        public async Task<ExchangeResult> GetExchangeRateAsync(ExchangeRateRequest request)
        {
            var result = await _exchangeRetriever.Retrieve(request);
            // TODO: handle currency not found
            return new ExchangeResult(request.To, _roundCalculator.EnsureRate(result?.Value ?? -1));
        }

        public Task<IEnumerable<Currency>> GetAvailableCurrenciesAsync()
        {
            return _exchangeRetriever.GetCurrenciesAsync();
        }

        protected virtual async Task<decimal> GetExchangeRateToBaseCurrency(Currency currency)
        {
            if (Equals(currency, _baseCurrency))
            {
                return 1;
            }

            var result = await _exchangeRetriever.Retrieve(new ExchangeRateRequest(currency, _baseCurrency));
            if (result != null)
            {
                return result.Value;
            }

            throw new CurrencyExchangeException(currency);
        }
    }
}