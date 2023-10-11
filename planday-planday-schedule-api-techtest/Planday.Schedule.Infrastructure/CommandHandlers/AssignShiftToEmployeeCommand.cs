using Dapper;
using Microsoft.Data.Sqlite;
using Planday.Schedule.Commands;
using Planday.Schedule.Infrastructure.DTOs;
using Planday.Schedule.Infrastructure.Helpers;
using Planday.Schedule.Infrastructure.Providers.Interfaces;

namespace Planday.Schedule.Infrastructure.CommandHandlers
{

    public class AssignShiftToEmployeeCommand : IAssignShiftToEmployeeCommand
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public AssignShiftToEmployeeCommand(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task<int> AssignShiftToEmployee(int shiftId, int employeeId)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var shiftDtos = await sqlConnection.QueryAsync<ShiftDto>(SqlQuery.SqlShift);
            var employeeDtos = await sqlConnection.QueryAsync<EmplyeeDto>(SqlQuery.SqlEmployee);

            var shift = shiftDtos.Select(x =>
                new Shift(x.Id, x.EmployeeId, DateTime.Parse(x.Start), DateTime.Parse(x.End))).Where(x => x.Id == shiftId).FirstOrDefault();

            var employeeShifts = shiftDtos.Select(x =>
                new Shift(x.Id, x.EmployeeId, DateTime.Parse(x.Start), DateTime.Parse(x.End))).Where(x => x.EmployeeId == employeeId);

            var employee = employeeDtos.Select(x =>
                new Employee(x.Id, x.Name)).Where(x => x.Id == employeeId).FirstOrDefault();

            if (employee == null || shift == null)
            {
                Console.Error.WriteLine("Employee ot Shift was not found");
                return 404;
            }

            if (shift?.EmployeeId != null)
            {
                Console.Error.WriteLine("You cannot assign the same shift to two or more employees.");
                return 400;
            }

            foreach (var emplyeeShift in employeeShifts)
            {
                if (emplyeeShift.End > shift?.Start)
                {
                    Console.Error.WriteLine("The employee must not have overlapping shifts.");
                    return 599;
                }
            }

            const string insertSql = "INSERT INTO Shift (EmployeeId) VALUES (@EmployeeId)";
            await sqlConnection.ExecuteAsync(insertSql, shift);

            return 200;
        }
    }
}
