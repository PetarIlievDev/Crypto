namespace WebApi.Models.Utilities
{
    using System.ComponentModel.DataAnnotations;

    public static class ModelValidator
    {
        public static ValidationResult? ValidatePositive(decimal value, string propName)
        {
            return value < 1 ? new ValidationResult(ErrorMessages.Number.IsNotPositive, [propName]) : null;
        }
        public static ValidationResult? ValidateNonEmptyStrings(string value, string propName)
        {
            return string.IsNullOrEmpty(value) ? new ValidationResult(ErrorMessages.String.IsEmpty, [propName]) : null;
        }
    }
}
