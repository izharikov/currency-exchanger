namespace CommerceExchanger.Core.Model
{
    public class ExchangeResult
    {
        public decimal Value { get; set; }
        public Currency Currency { get; set; }

        public ExchangeResult(Currency currency = null, decimal value = -1)
        {
            Value = value;
            Currency = currency;
        }
    }
}