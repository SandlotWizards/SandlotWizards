namespace SandlotWizards.Core.Interfaces
{
    /// <summary>
    /// Represents the outcome of a validation operation.
    /// </summary>
    public interface IValidationResult
    {
        /// <summary>
        /// Indicates whether the validation was successful.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// An optional message describing the reason for failure.
        /// </summary>
        string? ErrorMessage { get; }
    }
}
