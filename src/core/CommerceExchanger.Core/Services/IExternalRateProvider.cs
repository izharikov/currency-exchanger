using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services
{
    public interface IExternalRateProvider
    {
        string Name { get; }
        Task<IEnumerable<RateResponse>> Request(DateTimeOffset date);
        Task<IEnumerable<RateResponse>> Request(DateTimeOffset startDate, DateTimeOffset endDate);
        Currency BaseCurrency { get; }
    }
}