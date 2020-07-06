using System;

namespace CommerceExchanger.Core.Model
{
    public class ExchangeRequest: ExchangeRateRequest
    {
        public decimal Amount { get; set; } = 1;
    }
    
    public class ExchangeRateRequest
    {
        public Currency From { get; set; }
        public Currency To { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.Now;

        public ExchangeRateRequest(Currency from = null, Currency to = null)
        {
            From = from;
            To = to;
        }
    }
}