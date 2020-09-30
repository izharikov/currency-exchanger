using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Integrations.RateProviders.Extensions;
using CommerceExchanger.Utils.Extensions;

namespace CommerceExchanger.Integrations.RateProviders.Services
{
    /// <summary>
    ///     ECB rate provider
    ///     More info about using this API: https://sdw-wsrest.ecb.europa.eu/help/
    /// </summary>
    public class EcbRateProvider : IExternalRateProvider
    {
        /// <summary>
        ///     defines DateTime format for ECB API
        /// </summary>
        private const string DateFormat = "yyyy-MM-dd";

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(GenericData));

        private readonly IHttpClientFactory _httpClientFactory;

        public EcbRateProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public string Name => "ECB";

        public Task<IEnumerable<RateResponse>> Request(DateTimeOffset date)
        {
            return PerformRequest(BuildRequestUrl(string.Empty, date, date));
        }

        public Task<IEnumerable<RateResponse>> Request(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return PerformRequest(BuildRequestUrl(string.Empty, startDate, endDate));
        }

        public Currency BaseCurrency => Currency.EUR;

        private static string BuildRequestUrl(string to, DateTimeOffset startDate, DateTimeOffset endDate)
        {
            var url = new StringBuilder(73);
            // ECB query: key (Defining the dimension values)
            url.AppendFormat("D.{0}.EUR.SP00.A", to);
            // ECB query: startPeriod & endPeriod (Defining a date range)
            url.AppendFormat("?startPeriod={0}", startDate.ToString(DateFormat));
            url.AppendFormat("&endPeriod={0}", endDate.ToString(DateFormat));
            // ECB query: detail (Defining the amount of details)
            url.AppendFormat("&detail=dataonly");
            return url.ToString();
        }

        public static GenericData Deserialize(Stream xmlContent)
        {
            return Serializer.Deserialize(xmlContent) as GenericData;
        }

        private async Task<IEnumerable<RateResponse>> PerformRequest(string requestUrl)
        {
            var client = _httpClientFactory.GetRateHttpClient(typeof(EcbRateProvider));
            using var response =
                await client.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<RateResponse>();
            }

            await using var stream = await response.Content.ReadAsStreamAsync();
            if (stream.Length == 0)
            {
                return Enumerable.Empty<RateResponse>();
            }

            var genericData = Deserialize(stream);

            return genericData.DataSet.Series.SelectMany(x => x.Obs.Select(obs =>
            {
                DateTimeExtensions.Parse(obs.ObsDimension.Value, DateFormat, out var date);
                decimal.TryParse(obs.ObsValue.Value, out var value);
                var currencyFrom = x.SeriesKey.GenericValues.FirstOrDefault(x => x.Id == "CURRENCY_DENOM")?.Value;
                var currencyTo = x.SeriesKey.GenericValues.FirstOrDefault(x => x.Id == "CURRENCY")?.Value;
                return new RateResponse
                {
                    Date = date,
                    From = Currency.GetCurrency(currencyFrom),
                    To = Currency.GetCurrency(currencyTo),
                    Value = value
                };
            }));
        }
    }

    internal static class Namespaces
    {
        public const string Message = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/message";
        public const string Generic = "http://www.sdmx.org/resources/sdmxml/schemas/v2_1/data/generic";
    }

    [XmlRoot("GenericData", Namespace = Namespaces.Message)]
    public class GenericData
    {
        [XmlElement("DataSet", Namespace = Namespaces.Message)]
        public DataSet DataSet { get; set; }
    }

    [XmlRoot("DataSet", Namespace = Namespaces.Message)]
    public class DataSet
    {
        [XmlElement("Series", Namespace = Namespaces.Generic)]
        public Series[] Series { get; set; }
    }

    [XmlRoot("Series", Namespace = Namespaces.Generic)]
    public class Series
    {
        [XmlElement("SeriesKey", Namespace = Namespaces.Generic)]
        public SeriesKey SeriesKey { get; set; }

        [XmlElement("Obs", Namespace = Namespaces.Generic)]
        public Obs[] Obs { get; set; }
    }

    public class Obs
    {
        [XmlElement("ObsDimension", Namespace = Namespaces.Generic)]
        public ObsValue ObsDimension { get; set; }

        [XmlElement("ObsValue", Namespace = Namespaces.Generic)]
        public ObsValue ObsValue { get; set; }
    }

    public class ObsValue
    {
        [XmlAttribute("value")] public string Value { get; set; }
    }

    public class SeriesKey
    {
        [XmlElement("Value", Namespace = Namespaces.Generic)]
        public GenericValue[] GenericValues { get; set; }
    }

    public class GenericValue
    {
        [XmlAttribute("id")] public string Id { get; set; }
        [XmlAttribute("value")] public string Value { get; set; }
    }
}