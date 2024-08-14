namespace WebApi.Models.ChangeFromInitialBuy
{
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models.Utilities;

    public class ChangeFromInitialBuyDataRequest : IValidatableObject
    {
        public decimal NumberOfCoins { get; set; }
        public required string CryptoCurrencySymbol { get; set; }
        public decimal InitialBuyPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ModelValidator.ValidatePositive(NumberOfCoins, nameof(NumberOfCoins));
            yield return ModelValidator.ValidateNonEmptyStrings(CryptoCurrencySymbol, nameof(CryptoCurrencySymbol));
            yield return ModelValidator.ValidatePositive(InitialBuyPrice, nameof(InitialBuyPrice));
        }
    }
}
