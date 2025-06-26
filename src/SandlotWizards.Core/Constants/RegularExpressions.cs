namespace SandlotWizards.Core.Constants
{
    public static class RegularExpressions
    {
        public const string camelCase = "^[a-z]+([A-Z][a-z0-9]*)*$";
        public const string PascalCase = "^[A-Z][a-z0-9]*([A-Z][a-z0-9]*)*$";
        public const string snake_case = "^[a-z0-9]+(_[a-z0-9]+)*$";
        public const string kebobcase = "^[a-z0-9]+(-[a-z0-9]+)*$";
        public const string IPv4Address = "^((25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$\r\n";
        public const string MiddleInitial = @"^[A-Z]?$";
        public const string PhoneNumber = @"^\+?[1-9]\d{1,14}$";
        public const string PostalCode = @"^\d{5}(-\d{4})?$";
        public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string FirstName = "^[A-Z][a-z]*([ '-][A-Za-z]+)*$";
        public const string LastName = "^[A-Z][a-z]*([ '-][A-Za-z]+)*$";
        public const string UserId_NoPeriods = "^[a-zA-Z0-9_-]+$";
        public const string TextDescription_NoSpecialCharacters = "^[a-zA-Z0-9\\s.,'\"\\-()!?]+$";
        public const string RequestUri = "^\\/[a-z0-9\\/._-]*$";
    }
}
