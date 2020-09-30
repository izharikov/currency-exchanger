using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services
{
    /// <summary>
    ///     Storage for exchange rates
    /// </summary>
    public interface IExchangeRateStorage
    {
        /// <summary>
        ///     add or update exchange rate
        ///     in case, if the same <code>model</code> already exists in storage, update operation should be executed (no
        ///     Exception in this case)
        /// </summary>
        /// <param name="rates"></param>
        /// <param name="rateProvider"></param>
        /// <returns></returns>
        Task AddOrUpdateAsync(ICollection<RateResponse> rates, string rateProvider);

        /// <summary>
        ///     query exchange rate
        /// </summary>
        /// <param name="model">exchange rate request</param>
        /// <param name="rate">queried exchange rate</param>
        /// <param name="rateProvider"></param>
        /// <returns>true if rate successfully queries, false instead</returns>
        Task<bool> TryGetAsync(ExchangeRateRequest model, out decimal rate, string rateProvider);

        /// <summary>
        ///     get all available currencies
        /// </summary>
        /// <returns>collection of all currencies</returns>
        Task<ISet<Currency>> GetCurrenciesAsync(string rateProvider);
    }
}