using System;
using System.Collections.Generic;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Exceptions
{
    public class CurrencyExchangeException : ApplicationException
    {
        public CurrencyErrorType ErrorType { get; set; }
        public Currency[] Currencies { get; set; }

        private CurrencyExchangeException(string message, CurrencyErrorType errorType, params Currency[] currencies) : base(message)
        {
            ErrorType = errorType;
            Currencies = currencies;
        }

        public CurrencyExchangeException(Currency currency) : this($"Currency '{currency.Value}' not found.",
            CurrencyErrorType.CurrencyNotFound, currency)
        {
        }

        public CurrencyExchangeException(Currency from, Currency to) : this(
            $"Currency exchange from '{from.Value}' to '{to.Value}' not allowed.",
            CurrencyErrorType.CurrencyExchangeNotAllowed, from, to)
        {
        }

        public CurrencyExchangeExceptionJsonResult ToJson()
        {
            return new CurrencyExchangeExceptionJsonResult
            {
                Error = ErrorType.ToString(),
                Message = Message,
                Currencies = Currencies
            };
        }
    }

    public class CurrencyExchangeExceptionJsonResult
    {
        public string Error { get; set; }
        public string Message { get; set; }
        public IList<Currency> Currencies { get; set; }
    }
}