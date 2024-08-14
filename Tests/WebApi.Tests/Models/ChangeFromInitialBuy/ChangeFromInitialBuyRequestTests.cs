namespace WebApi.Tests.Models.ChangeFromInitialBuy
{
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models.ChangeFromInitialBuy;
    using WebApi.Models.Utilities;

    internal class ChangeFromInitialBuyRequestTests
{
    [Test]
    public void ValidateNullableCryptoCurrencySymbol()
    {
        var request = new ChangeFromInitialBuyDataRequest()
        {
            InitialBuyPrice = 1,
            CryptoCurrencySymbol = string.Empty,
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
        var request = new ChangeFromInitialBuyDataRequest()
        {
            InitialBuyPrice = initialBuyPrice,
            CryptoCurrencySymbol = "BTC",
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
        var request = new ChangeFromInitialBuyDataRequest()
        {
            InitialBuyPrice = 1,
            CryptoCurrencySymbol = "BTC",
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
        var request = new ChangeFromInitialBuyDataRequest()
        {
            InitialBuyPrice = 1,
            CryptoCurrencySymbol = "BTC",
            NumberOfCoins = 1
        };
        ValidationContext context = new(request);
        var results = request.Validate(context);
        Assert.That(results.FirstOrDefault(x => x != null), Is.Null);
    }
}
}
