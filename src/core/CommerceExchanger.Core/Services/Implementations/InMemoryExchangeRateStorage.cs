using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services.Implementations
{
    public class InMemoryExchangeRateStorage : IExchangeRateStorage
    {
        private readonly IDictionary<string, decimal> _storage = new Dictionary<string, decimal>();
        private readonly ISet<Currency> _currencies = new HashSet<Currency>();

        public Task AddOrUpdate(ExchangeRateRequest model, decimal rate)
        {
            Add(model, rate);
            return Task.CompletedTask;
        }

        public void Add(ExchangeRateRequest model, decimal rate)
        {
            _storage[GetCacheKey(model)] = rate;
            _currencies.Add(model.From);
            _currencies.Add(model.To);
        }

        public Task<bool> Get(ExchangeRateRequest model, out decimal rate)
        {
            return Task.FromResult(_storage.TryGetValue(GetCacheKey(model), out rate));
        }

        public Task<IEnumerable<Currency>> GetCurrencies()
        {
            return Task.FromResult((IEnumerable<Currency>) _currencies);
        }

        private static string GetCacheKey(ExchangeRateRequest request)
        {
            return $"{request.From.Value}_{request.To.Value}";
        }
    }
}