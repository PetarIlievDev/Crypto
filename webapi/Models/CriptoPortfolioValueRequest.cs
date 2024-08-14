namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models.Utilities;

    public class CriptoPortfolioValueRequest : IValidatableObject
    {
        public decimal NumberOfCoins { get; set; }
        public required string CryptoCurrency { get; set; }
        public decimal InitialBuyPrice { get; set; }

        public IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
        {
            yield return ModelValidator.ValidatePositive(NumberOfCoins, nameof(NumberOfCoins));
            yield return ModelValidator.ValidateNonEmptyStrings(CryptoCurrency, nameof(CryptoCurrency));
            yield return ModelValidator.ValidatePositive(InitialBuyPrice, nameof(InitialBuyPrice));
        }
    }
}
