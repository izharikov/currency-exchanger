using System.Threading.Tasks;
using CommerceExchanger.Core.Arithmetic;
using CommerceExchanger.Core.Exceptions;
using CommerceExchanger.Core.Model;
using CommerceExchanger.Core.Services;
using CommerceExchanger.Core.Services.Base;
using Moq;
using Xunit;

namespace CommerceExchanger.Core.Test.Services
{
    public class ExchangerTest
    {
        public ExchangerTest()
        {
            var mock =
                new Mock<IExchangeRateProvider>();
            mock
                .Setup(x => x.GetExchangeRateAsync(
                    It.Is<ExchangeRateRequest>(m =>
                        Equals(m.From, Currency.EUR) && Equals(m.To, Currency.BYN))))
                .Returns(Task.FromResult(
                    new ExchangeResult(Currency.BYN, SampleExchangeRate)));
            _exchanger = new Exchanger(new RoundCalculator(), mock.Object);
        }

        private readonly IExchanger _exchanger;

        private const decimal SampleExchangeRate = 0.23M;

        [Fact]
        public async Task ExchangeShouldThrowException()
        {
            var exception = await Assert.ThrowsAsync<CurrencyExchangeException>(async () =>
                await _exchanger.ExchangeAsync(new ExchangeRateRequest(Currency.USD, Currency.BYN), 10));
            Assert.Equal(CurrencyErrorType.CurrencyExchangeNotAllowed, exception.ErrorType);
        }

        [Fact]
        public async Task ValidateExchange()
        {
            Assert.Equal(SampleExchangeRate * 10,
                (await _exchanger.ExchangeAsync(new ExchangeRateRequest(Currency.EUR, Currency.BYN), 10)).Value, 4);
        }
    }
}