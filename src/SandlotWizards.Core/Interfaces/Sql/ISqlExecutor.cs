using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Interfaces.Sql;

/// <summary>
/// Executes raw SQL commands using a named connection.
/// </summary>
public interface ISqlExecutor
{
    /// <summary>
    /// Executes the given SQL script against the specified connection key.
    /// </summary>
    /// <param name="connectionKey">The key used to identify the connection string.</param>
    /// <param name="sql">The SQL script to execute.</param>
    /// <param name="ct">Cancellation token.</param>
    Task ExecuteAsync(string connectionKey, string sql, CancellationToken ct);

    /// <summary>
    /// Executes a query that returns a collection of values from a specific column.
    /// </summary>
    /// <param name="connectionKey">Key for the connection string dictionary.</param>
    /// <param name="sql">The SELECT query to execute.</param>
    /// <param name="columnName">The name of the column to extract values from.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A list of string values from the specified column.</returns>
    Task<IEnumerable<string>> ExecuteReaderAsync(string connectionKey, string sql, string columnName, CancellationToken ct);
}
