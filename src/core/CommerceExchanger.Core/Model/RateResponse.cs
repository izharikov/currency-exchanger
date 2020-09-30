using System;
using System.Text.Json.Serialization;
using CommerceExchanger.Core.Json.Converters;

namespace CommerceExchanger.Core.Model
{
    public class RateResponse
    {
        public Currency From { get; set; }
        public Currency To { get; set; }
        public decimal Value { get; set; }

        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset Date { get; set; }

        public bool Temporary { get; set; }
    }
}