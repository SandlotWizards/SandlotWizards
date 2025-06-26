using SandlotWizards.Core.Interfaces;

namespace SandlotWizards.Core.Models
{
    /// <summary>
    /// Represents the result of a validation operation, indicating success or failure and an optional error message.
    /// </summary>
    public sealed class ValidationResult : IValidationResult
    {
        /// <summary>
        /// Gets a value indicating whether the validation was successful.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Gets an optional error message if the validation failed.
        /// </summary>
        public string? ErrorMessage { get; }

        private ValidationResult(bool isValid, string? errorMessage = null)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a successful validation result.
        /// </summary>
        public static ValidationResult Success() => new(true);

        /// <summary>
        /// Creates a failed validation result with the specified error message.
        /// </summary>
        /// <param name="errorMessage">The reason the validation failed.</param>
        public static ValidationResult Fail(string errorMessage) => new(false, errorMessage);
    }
}
