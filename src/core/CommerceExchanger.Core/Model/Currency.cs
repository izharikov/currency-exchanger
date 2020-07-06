using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CommerceExchanger.Core.Model
{
    [JsonConverter(typeof(CurrencyJsonConverter))]
    [DebuggerDisplay("Currency = {" + nameof(Value) + "}")]
    public class Currency
    {
        public string Value { get; }

        public Currency(string value)
        {
            Value = value;
        }

        protected bool Equals(Currency other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Currency) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static readonly Currency USD = new Currency("USD");
        public static readonly Currency EUR = new Currency("EUR");
        public static readonly Currency BYN = new Currency("BYN");
    }

    public class CurrencyJsonConverter : JsonConverter<Currency>
    {
        public override Currency Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Currency(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Currency value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}