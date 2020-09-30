using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using CommerceExchanger.Core.Json.Converters;

namespace CommerceExchanger.Core.Model
{
    [JsonConverter(typeof(CurrencyJsonConverter))]
    [DebuggerDisplay("Currency = {" + nameof(Value) + "}")]
    public class Currency
    {
        private static readonly IDictionary<string, Currency> _currencies = new Dictionary<string, Currency>();

        public static readonly Currency USD = GetCurrency("USD");
        public static readonly Currency EUR = GetCurrency("EUR");
        public static readonly Currency BYN = GetCurrency("BYN");
        public static readonly Currency RUB = GetCurrency("RUB");

        public static readonly IEqualityComparer<Currency> DefaultComparer = new CurrencyEqualityComparer();

        private Currency(string value)
        {
            Value = value;
        }

        public string Value { get; }

        protected bool Equals(Currency other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Currency) obj);
        }

        public override int GetHashCode()
        {
            return Value != null ? Value.GetHashCode() : 0;
        }

        public override string ToString()
        {
            return $"[{Value}]";
        }

        public static Currency GetCurrency(string code)
        {
            if (_currencies.ContainsKey(code))
            {
                return _currencies[code];
            }

            return _currencies[code] = new Currency(code);
        }

        public static IEnumerable<Currency> GetAll()
        {
            return _currencies.Values;
        }

        private class CurrencyEqualityComparer : IEqualityComparer<Currency>
        {
            public bool Equals(Currency x, Currency y)
            {
                return x != null && x.Equals(y);
            }

            public int GetHashCode(Currency obj)
            {
                return obj?.GetHashCode() ?? 0;
            }
        }
    }
}