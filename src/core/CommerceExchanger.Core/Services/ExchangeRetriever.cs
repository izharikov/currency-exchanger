using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Utils.Extensions;

namespace CommerceExchanger.Core.Services
{
    public class ExchangeRetriever
    {
        private readonly IExchangeRateStorage _exchangeRateStorage;
        private readonly IEnumerable<IExternalRateProvider> _externalRateProviders;

        private readonly IDictionary<string, ISet<Currency>> _providersCurrency =
            new Dictionary<string, ISet<Currency>>();

        private readonly IDictionary<string, (IExternalRateProvider, IExternalRateProvider, Currency)> _providerCache =
            new Dictionary<string, (IExternalRateProvider, IExternalRateProvider, Currency)>();

        public ExchangeRetriever(IExchangeRateStorage exchangeRateStorage,
            IEnumerable<IExternalRateProvider> externalRateProviders)
        {
            _exchangeRateStorage = exchangeRateStorage;
            _externalRateProviders = externalRateProviders;
        }

        public async Task<IEnumerable<Currency>> GetCurrenciesAsync()
        {
            return (await _externalRateProviders.Select(GetProviderCurrencies)).SelectMany(x => x).Distinct();
        }

        private async Task<ISet<Currency>> GetProviderCurrencies(IExternalRateProvider externalRateProvider)
        {
            if (!_providersCurrency.ContainsKey(externalRateProvider.Name))
            {
                var currencies = await _exchangeRateStorage.GetCurrenciesAsync(externalRateProvider.Name);
                if (!currencies.Any())
                {
                    var rates = await Request(externalRateProvider, DateTimeOffset.Now);
                    await _exchangeRateStorage.AddOrUpdateAsync(rates.ToList(), externalRateProvider.Name);
                    currencies = rates.SelectMany(x => new[] {x.From, x.To}).ToHashSet();
                }

                _providersCurrency[externalRateProvider.Name] = currencies;
            }

            return _providersCurrency[externalRateProvider.Name];
        }

        public async Task<RateResponse> Retrieve(ExchangeRateRequest request)
        {
            var (fromProvider, toProvider, baseCurrency) = await GetCachedProvider(request);
            if (fromProvider == null)
            {
                return null;
            }

            async Task<decimal?> QueryBySingleProvider(IExternalRateProvider provider, Currency from, Currency to)
            {
                var first = await GetFromStorage(new ExchangeRateRequest(from, provider.BaseCurrency, request.Date),
                    provider.Name);
                var second = await GetFromStorage(new ExchangeRateRequest(provider.BaseCurrency, to, request.Date),
                    provider.Name);
                return first * second;
            }


            async Task<RateResponse> QueryStorage()
            {
                decimal? value;
                if (fromProvider == toProvider)
                {
                    value = await QueryBySingleProvider(fromProvider, request.From, request.To);
                }
                else
                {
                    var first = await QueryBySingleProvider(fromProvider, request.From, baseCurrency);
                    var second = await QueryBySingleProvider(toProvider, baseCurrency, request.To);
                    value = first * second;
                }

                return value == null
                    ? null
                    : new RateResponse()
                    {
                        From = request.From,
                        To = request.To,
                        Date = request.Date,
                        Value = value.Value
                    };
            }

            var rateResponse = await QueryStorage();
            if (rateResponse != null)
            {
                return rateResponse;
            }


            foreach (var provider in new[] {fromProvider, toProvider}.Distinct())
            {
                var rates = await Request(provider, request.Date);
                await _exchangeRateStorage.AddOrUpdateAsync(rates.ToList(), provider.Name);
            }

            return await QueryStorage();
        }

        private async Task<IList<RateResponse>> Request(IExternalRateProvider provider, DateTimeOffset date)
        {
            var result = await provider.Request(date);
            var resultList = result.ToList();
            if (resultList.Any())
            {
                return resultList;
            }

            var slideWindow = (await provider.Request(date.AddDays(-5), date)).ToList();
            var delta = -5;
            while (!slideWindow.Any())
            {
                slideWindow = (await provider.Request(date.AddDays(delta), date)).ToList();
                delta -= 5;
            }

            var maxDate = slideWindow.Select(x => x.Date).Max();
            var maxDateRates = slideWindow.Where(x => x.Date == maxDate).ToList();
            for (var i = 1; i <= date.Subtract(maxDate).Days; i++)
            {
                var currentDate = maxDate.AddDays(i);
                var isTemporary = currentDate.AddDays(1) > date;
                slideWindow.AddRange(maxDateRates.Select(x => new RateResponse
                {
                    From = x.From,
                    To = x.To,
                    Date = maxDate.AddDays(i),
                    Value = x.Value,
                    Temporary = isTemporary
                }));
            }

            return slideWindow;
        }

        private async ValueTask<decimal?> GetFromStorage(ExchangeRateRequest request, string providerName)
        {
            if (request.From.Equals(request.To))
            {
                return 1;
            }

            if (await _exchangeRateStorage.TryGetAsync(request, out var rate, providerName))
            {
                return rate;
            }

            return null;
        }

        private async Task<(IExternalRateProvider, IExternalRateProvider, Currency)> GetCachedProvider(
            ExchangeRateRequest request)
        {
            var cacheKey = $"{request.From}_{request.To}";
            if (!_providerCache.ContainsKey(cacheKey))
            {
                _providerCache[cacheKey] = await GetProvider(request);
            }

            return _providerCache[cacheKey];
        }

        private async Task<(IExternalRateProvider, IExternalRateProvider, Currency)> GetProvider(
            ExchangeRateRequest request)
        {
            var fromCurrencyProviders = new List<(IExternalRateProvider, ISet<Currency>)>();
            var toCurrencyProviders = new List<(IExternalRateProvider, ISet<Currency>)>();
            foreach (var externalRateProvider in _externalRateProviders)
            {
                var currencies = await GetProviderCurrencies(externalRateProvider);
                if (currencies.Contains(request.From) && currencies.Contains(request.To))
                {
                    return (externalRateProvider, externalRateProvider, null);
                }

                if (currencies.Contains(request.From))
                {
                    fromCurrencyProviders.Add((externalRateProvider, currencies));
                }

                if (currencies.Contains(request.To))
                {
                    toCurrencyProviders.Add((externalRateProvider, currencies));
                }
            }

            foreach (var (fromCurrencyProvider, currenciesFrom) in fromCurrencyProviders)
            {
                foreach (var (toCurrencyProvider, currenciesTo) in toCurrencyProviders)
                {
                    var currency = currenciesFrom.Intersect(currenciesTo).FirstOrDefault();
                    if (currency != null)
                    {
                        return (fromCurrencyProvider, toCurrencyProvider, currency);
                    }
                }
            }

            return (null, null, null);
        }
    }
}