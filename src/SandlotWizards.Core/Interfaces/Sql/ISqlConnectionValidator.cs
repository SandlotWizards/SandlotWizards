using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Interfaces.Sql
{
    /// <summary>
    /// Defines methods for validating and executing raw SQL connections and statements.
    /// </summary>
    public interface ISqlConnectionValidator
    {
        /// <summary>
        /// Attempts to connect to a SQL database using the provided connection string.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>True if the connection succeeds; otherwise, false.</returns>
        Task<bool> CanConnectAsync(string connectionString, CancellationToken ct);

        /// <summary>
        /// Executes a raw SQL statement against the specified connection string.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        /// <param name="sql">The SQL command text to execute.</param>
        /// <param name="ct">Cancellation token.</param>
        Task ExecuteRawSqlAsync(string connectionString, string sql, CancellationToken ct);
    }
}
