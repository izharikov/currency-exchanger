using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Integrations.RateProviders.Extensions;

namespace CommerceExchanger.Integrations.RateProviders.Services
{
    public class CbrRateProvider : IExternalRateProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CbrRateProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string Name => "CBR";

        public Task<IEnumerable<RateResponse>> Request(DateTimeOffset date)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RateResponse>> Request(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public Currency BaseCurrency => Currency.RUB;

        private async Task<IEnumerable<RateResponse>> PerformRequest(string requestUrl)
        {
            var client = _httpClientFactory.GetRateHttpClient(GetType());
            using var response = await client.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<RateResponse>();
            }

            await using var stream = await response.Content.ReadAsStreamAsync();
            if (stream.Length == 0)
            {
                return Enumerable.Empty<RateResponse>();
            }

            using var reader = XmlReader.Create(stream, new XmlReaderSettings
            {
                Async = true
            });
            RateResponse current = null;
            string from = null;
            string to = null;
            while (await reader.ReadAsync())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("Valute"))
                        {
                            current = new RateResponse();
                        }

                        if (reader.Name.Equals("CharCode"))
                        {
                            current.To = Currency.GetCurrency(reader.Value);
                        }

                        break;
                }
            }

            return new List<RateResponse>();
        }
    }
}