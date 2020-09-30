using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Json.Converters
{
    public class CurrencyJsonConverter : JsonConverter<Currency>
    {
        public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return Currency.GetCurrency(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Currency value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}