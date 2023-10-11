using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Commands;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using System.Web.Http;

namespace Planday.Schedule.Infrastructure.CommandHandlers
{
    public class CreateOpenShiftAsyncCommand : ICreateOpenShift
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public CreateOpenShiftAsyncCommand(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task<bool> CreateOpenShiftAsync<T>(T shift)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            const string insertSql = "INSERT INTO Shift (Start, End) VALUES (@Start, @End)";

            if (shift != null)
            {
                await sqlConnection.ExecuteAsync(insertSql, shift);
                return true;
            }
            else
            {
                Console.Error.WriteLine("Shift is empty");
                return false;
            }
        }
    }
}
