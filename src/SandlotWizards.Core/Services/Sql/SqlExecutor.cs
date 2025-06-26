using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SandlotWizards.Core.Interfaces.Sql;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Services.Sql;

/// <summary>
/// Executes raw SQL commands and queries against configured connection strings.
/// </summary>
public class SqlExecutor : ISqlExecutor
{
    private readonly IDictionary<string, string> _connectionStrings;
    private readonly ILogger<SqlExecutor> _logger;

    /// <summary>
    /// Constructs a new SqlExecutor.
    /// </summary>
    /// <param name="connectionStrings">Dictionary of named connection strings.</param>
    /// <param name="logger">Logger instance.</param>
    public SqlExecutor(IDictionary<string, string> connectionStrings, ILogger<SqlExecutor> logger)
    {
        _connectionStrings = connectionStrings;
        _logger = logger;
    }

    /// <summary>
    /// Executes a non-query SQL command using the specified connection key.
    /// </summary>
    /// <param name="connectionKey">Key for the connection string dictionary.</param>
    /// <param name="sql">SQL script to execute.</param>
    /// <param name="ct">Cancellation token.</param>
    public async Task ExecuteAsync(string connectionKey, string sql, CancellationToken ct)
    {
        if (!_connectionStrings.TryGetValue(connectionKey, out var connectionString))
            throw new ArgumentException($"Connection string key '{connectionKey}' not found.", nameof(connectionKey));

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(ct);

        await using var command = new SqlCommand(sql, connection);
        await command.ExecuteNonQueryAsync(ct);
    }

    /// <summary>
    /// Executes a SQL query that returns values from a specific column.
    /// </summary>
    /// <param name="connectionKey">Key for the connection string dictionary.</param>
    /// <param name="sql">SELECT query to execute.</param>
    /// <param name="columnName">Column name to extract from the result set.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>List of values from the specified column.</returns>
    public async Task<IEnumerable<string>> ExecuteReaderAsync(string connectionKey, string sql, string columnName, CancellationToken ct)
    {
        if (!_connectionStrings.TryGetValue(connectionKey, out var connectionString))
            throw new ArgumentException($"Connection string key '{connectionKey}' not found.", nameof(connectionKey));

        var results = new List<string>();

        await using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync(ct);

        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync(ct);

        var ordinal = reader.GetOrdinal(columnName);
        while (await reader.ReadAsync(ct))
        {
            if (!reader.IsDBNull(ordinal))
            {
                results.Add(reader.GetString(ordinal));
            }
        }

        return results;
    }
}
