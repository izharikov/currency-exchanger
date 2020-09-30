using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Integrations.RateProviders.Extensions;
using CommerceExchanger.Utils.Extensions;

namespace CommerceExchanger.Integrations.RateProviders.Services
{
    /// <summary>
    ///     API for NBRB (Belarus): https://www.nbrb.by/apihelp/exrates
    /// </summary>
    public class NbrbRateProvider : IExternalRateProvider
    {
        /// <summary>
        ///     defines DateTime format for request NBRB API
        /// </summary>
        private const string DateFormatRequest = "yyyy-MM-dd";

        private const string DateFormatResponse = "yyyy-MM-ddTHH:mm:ss";

        private readonly IHttpClientFactory _httpClientFactory;

        public NbrbRateProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string Name => "NBRB";

        public Task<IEnumerable<RateResponse>> Request(DateTimeOffset date)
        {
            return PerformRequest($"?ondate={date.ToString(DateFormatRequest)}&periodicity=0");
        }

        public async Task<IEnumerable<RateResponse>> Request(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var res = new List<Task<IEnumerable<RateResponse>>>();
            for (var day = startDate.Date; day.Date <= endDate.Date; day = day.AddDays(1))
            {
                res.Add(Request(day));
            }

            return (await res).SelectMany(x => x);
        }

        public Currency BaseCurrency => Currency.BYN;

        private async Task<IEnumerable<RateResponse>> PerformRequest(string requestParams)
        {
            var client = _httpClientFactory.GetRateHttpClient(typeof(NbrbRateProvider));
            using var response = await client.GetAsync(requestParams);
            await using var resultStream = await response.Content.ReadAsStreamAsync();
            var parsedResult = await JsonSerializer.DeserializeAsync<NbrbCurrencyResponse[]>(resultStream);
            return parsedResult.Select(x => new RateResponse
            {
                Date = DateTimeExtensions.Parse(x.Date, DateFormatResponse, out var dateResult)
                    ? dateResult
                    : DateTimeOffset.MinValue,
                From = Currency.BYN,
                To = Currency.GetCurrency(x.CurAbbreviation),
                Value = x.CurrencyScale / x.CurrencyOfficialRate
            });
        }

        private class NbrbCurrencyResponse
        {
            [JsonPropertyName("Cur_Abbreviation")] public string CurAbbreviation { get; set; }
            [JsonPropertyName("Cur_Scale")] public int CurrencyScale { get; set; }
            [JsonPropertyName("Cur_OfficialRate")] public decimal CurrencyOfficialRate { get; set; }
            public string Date { get; set; }
        }
    }
}