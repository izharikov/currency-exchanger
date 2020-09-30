using System;

namespace CommerceExchanger.Integrations.Storage.Models
{
    public class ExchangeRateModel
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public DateTime DateRate { get; set; }
        public string Provider { get; set; }
    }
}