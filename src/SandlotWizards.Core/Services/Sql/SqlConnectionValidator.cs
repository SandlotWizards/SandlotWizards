using Microsoft.Data.SqlClient;
using SandlotWizards.Core.Interfaces.Sql;
using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Services.Sql
{
    /// <summary>
    /// Provides methods to validate and execute SQL operations.
    /// </summary>
    public class SqlConnectionValidator : ISqlConnectionValidator
    {
        /// <inheritdoc/>
        public async Task<bool> CanConnectAsync(string connectionString, CancellationToken ct)
        {
            try
            {
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync(ct);
                return connection.State == System.Data.ConnectionState.Open;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public async Task ExecuteRawSqlAsync(string connectionString, string sql, CancellationToken ct)
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync(ct);

            await using var command = connection.CreateCommand();
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync(ct);
        }
    }
}
