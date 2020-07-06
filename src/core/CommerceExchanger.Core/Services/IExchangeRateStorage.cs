using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services
{
    public interface IExchangeRateStorage
    {
        Task AddOrUpdate(ExchangeRateRequest model, decimal rate);
        Task<bool> Get(ExchangeRateRequest model, out decimal rate);
        Task<IEnumerable<Currency>> GetCurrencies();
    }
}