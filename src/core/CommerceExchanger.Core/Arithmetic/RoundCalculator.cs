using System;

namespace CommerceExchanger.Core.Arithmetic
{
    public class RoundCalculator
    {
        private const int AmountDecimalDefaultCount = 2;
        private const int ExchangeDecimalDefaultCount = 4;

        private readonly int _amountDecimalCount;
        private readonly int _exchangeDecimalCount;
        
        public RoundCalculator()
        {
            _amountDecimalCount = AmountDecimalDefaultCount;
            _exchangeDecimalCount = ExchangeDecimalDefaultCount;
        }

        public RoundCalculator(int amountDecimalCount, int exchangeDecimalCount)
        {
            _amountDecimalCount = amountDecimalCount;
            _exchangeDecimalCount = exchangeDecimalCount;
        }

        public decimal EnsureRate(decimal rate)
        {
            return Math.Round(rate, _exchangeDecimalCount);
        }
        
        public decimal EnsureExchange(decimal amount, decimal rate)
        {
            return Math.Round(amount * rate, _amountDecimalCount);
        }
    }
}