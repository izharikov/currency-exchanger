using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services.Implementations
{
    public class InMemoryExchangeRateStorage : IExchangeRateStorage
    {
        private readonly IDictionary<string, ISet<Currency>> _currencies = new Dictionary<string, ISet<Currency>>();
        private readonly IDictionary<string, decimal> _storage = new Dictionary<string, decimal>();

        public Task AddOrUpdateAsync(ICollection<RateResponse> rates, string rateProvider)
        {
            if (!rates.Any())
            {
                return Task.CompletedTask;
            }

            foreach (var rate in rates)
            {
                Add(new ExchangeRateRequest(rate.From, rate.To, rate.Date), rate.Value, rateProvider);
                Add(new ExchangeRateRequest(rate.To, rate.From, rate.Date), 1 / rate.Value, rateProvider);
            }

            return Task.CompletedTask;
        }

        public Task<bool> TryGetAsync(ExchangeRateRequest model, out decimal rate, string rateProvider)
        {
            return Task.FromResult(_storage.TryGetValue(GetCacheKey(model, rateProvider), out rate));
        }

        public Task<ISet<Currency>> GetCurrenciesAsync(string rateProvider)
        {
            if (rateProvider == null)
            {
                throw new ArgumentException(nameof(rateProvider));
            }

            ISet<Currency> result = null;
            if (_currencies.ContainsKey(rateProvider))
            {
                result = _currencies[rateProvider];
            }
            return Task.FromResult(result ?? new HashSet<Currency>());
        }

        public void Add(ExchangeRateRequest model, decimal rate, string rateProvider)
        {
            _storage[GetCacheKey(model, rateProvider)] = rate;
            if (!_currencies.ContainsKey(rateProvider))
            {
                _currencies[rateProvider] = new HashSet<Currency>();
            }
            _currencies[rateProvider].Add(model.From);
            _currencies[rateProvider].Add(model.To);
        }

        private static string GetCacheKey(ExchangeRateRequest request, string rateProvider)
        {
            return $"{rateProvider}_{request.From.Value}_{request.To.Value}_{request.Date:d}";
        }
    }
}