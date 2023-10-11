using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Infrastructure.DTOs;
using Planday.Schedule.Infrastructure.Helpers;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class GetAllShiftsQuery : IGetAllShiftsQuery
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public GetAllShiftsQuery(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }
    
        public async Task<IReadOnlyCollection<Shift>> QueryAsync()
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var shiftDtos = await sqlConnection.QueryAsync<ShiftDto>(SqlQuery.SqlShift);

            var shifts = shiftDtos.Select(x => 
                new Shift(x.Id, x.EmployeeId, DateTime.Parse(x.Start), DateTime.Parse(x.End)));
        
            return shifts.ToList();
        }
    }    
}

