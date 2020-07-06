using System.Threading.Tasks;
using CommerceExchanger.Core.Model;

namespace CommerceExchanger.Core.Services
{
    public interface IExchanger
    {
        Task<ExchangeResult> Exchange(ExchangeRequest request);
    }
}