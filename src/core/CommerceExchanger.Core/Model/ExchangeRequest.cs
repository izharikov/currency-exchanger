using System;

namespace CommerceExchanger.Core.Model
{
    public struct ExchangeRateRequest
    {
        public Currency From { get; }
        public Currency To { get; }
        public DateTimeOffset Date { get; }

        public ExchangeRateRequest(Currency from = null, Currency to = null, DateTimeOffset? date = null)
        {
            From = from;
            To = to;
            Date = date ?? DateTimeOffset.Now;
        }
    }
}