using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services
{
    public interface IExchangeRateProvider
    {
        Task<ExchangeResult> GetExchangeRateAsync(ExchangeRateRequest request);
        Task<IEnumerable<Currency>> GetAvailableCurrenciesAsync();
    }
}