using System.Threading.Tasks;
using CommerceExchanger.Core.Arithmetic;
using CommerceExchanger.Core.Exceptions;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services.Base
{
    public class Exchanger : IExchanger
    {
        protected RoundCalculator RoundCalculator;
        protected IExchangeRateProvider ExchangeRateProvider;

        public Exchanger(RoundCalculator roundCalculator, IExchangeRateProvider exchangeRateProvider)
        {
            RoundCalculator = roundCalculator;
            ExchangeRateProvider = exchangeRateProvider;
        }

        public async Task<ExchangeResult> Exchange(ExchangeRequest request)
        {
            var initialRate = await GetRate(request);
            var rate = RoundCalculator.EnsureRate(initialRate);
            var amount = RoundCalculator.EnsureExchange(request.Amount, rate);
            return new ExchangeResult(request.To, amount);
        }

        private async Task<decimal> GetRate(ExchangeRateRequest request)
        {
            if (request.From.Equals(request.To))
            {
                return 1;
            }
            var initialRate = (await ExchangeRateProvider.GetExchangeRate(request)).Value;
            if (initialRate > 0)
            {
                return initialRate;
            }

            throw new CurrencyExchangeException(request.From, request.To);
        }
    }
}