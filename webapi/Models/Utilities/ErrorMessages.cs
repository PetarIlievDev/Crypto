namespace WebApi.Models.Utilities
{
    public static class ErrorMessages
    {
        public static class Number
        {
            public const string IsNotPositive = "Invalid value. Please provide a positive number.";
        }

        public static class String
        {
            public const string IsEmpty = "Ivalid value. Please, make sure currency name contains at least one character";
        }
    }
}
