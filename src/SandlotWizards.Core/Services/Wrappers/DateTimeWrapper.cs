using SandlotWizards.Core.Interfaces.Wrapper;
using System;

namespace SandlotWizards.Core.Services.Wrappers
{
    /// <summary>
    /// Provides a wrapper around <see cref="DateTime"/> for abstraction and testability.
    /// </summary>
    public class DateTimeWrapper : IDateTime
    {
        /// <summary>
        /// Gets the current local date and time.
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Gets the current local date with the time component set to 00:00:00.
        /// </summary>
        public DateTime Today => DateTime.Today;

        /// <summary>
        /// Gets a sentinel value used to represent a null or uninitialized date.
        /// </summary>
        public DateTime Null => new DateTime(1800, 1, 1);

        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
