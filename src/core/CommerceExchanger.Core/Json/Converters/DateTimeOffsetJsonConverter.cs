using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommerceExchanger.Utils.Extensions;

namespace CommerceExchanger.Core.Json.Converters
{
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (DateTimeExtensions.Parse(reader.GetString(), "yyyy-MM-dd", out var date))
            {
                return date;
            }

            return DateTimeOffset.MinValue;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}