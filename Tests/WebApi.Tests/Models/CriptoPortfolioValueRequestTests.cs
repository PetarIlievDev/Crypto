namespace WebApi.Tests.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WebApi.Models;
    using WebApi.Models.Utilities;

    public class CriptoPortfolioValueRequestTests
    {
        [Test]
        public void ValidateNullableCryptoCurrency()
        {
            var request = new CriptoPortfolioValueRequest() 
            { 
                InitialBuyPrice = 1,
                CryptoCurrency = string.Empty,
                NumberOfCoins = 1
            };
            ValidationContext context = new(request);
            var results = request.Validate(context);
            Assert.That(results, Is.Not.Null);
            Assert.That(results.First(x => x != null).ErrorMessage, Is.EqualTo(ErrorMessages.String.IsEmpty));
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void ValidateLessThanOneInitialBuyPrice(decimal initialBuyPrice)
        {
            var request = new CriptoPortfolioValueRequest()
            {
                InitialBuyPrice = initialBuyPrice,
                CryptoCurrency = "BTC",
                NumberOfCoins = 1
            };
            ValidationContext context = new(request);
            var results = request.Validate(context);
            Assert.That(results, Is.Not.Null);
            Assert.That(results.First(x => x != null).ErrorMessage, Is.EqualTo(ErrorMessages.Number.IsNotPositive));
        }

        [TestCase(-1.0)]
        [TestCase(0.00)]
        public void ValidateLessThanOneNumberOfCoins(decimal numberOfCoins)
        {
            var request = new CriptoPortfolioValueRequest()
            {
                InitialBuyPrice = 1,
                CryptoCurrency = "BTC",
                NumberOfCoins = numberOfCoins
            };
            ValidationContext context = new(request);
            var results = request.Validate(context);
            Assert.That(results, Is.Not.Null);
            Assert.That(results.First(x => x != null).ErrorMessage, Is.EqualTo(ErrorMessages.Number.IsNotPositive));
        }

        [Test]
        public void ValidateWhenAllValidationsPass()
        {
            var request = new CriptoPortfolioValueRequest()
            {
                InitialBuyPrice = 1,
                CryptoCurrency = "BTC",
                NumberOfCoins = 1
            };
            ValidationContext context = new(request);
            var results = request.Validate(context);
            Assert.That(results.FirstOrDefault(x => x != null), Is.Null);
        }
    }
}
