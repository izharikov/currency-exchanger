namespace CommerceExchanger.Core.Model
{
    public struct ExchangeResult
    {
        public decimal Value { get; }
        public Currency Currency { get; }

        public ExchangeResult(Currency currency = null, decimal value = -1)
        {
            Value = value;
            Currency = currency;
        }
    }
}