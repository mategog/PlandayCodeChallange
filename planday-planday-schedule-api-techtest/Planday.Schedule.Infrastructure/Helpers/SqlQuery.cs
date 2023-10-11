namespace Planday.Schedule.Infrastructure.Helpers
{
    internal abstract class SqlQuery
    {
        internal const string SqlShift = @"SELECT Id, EmployeeId, Start, End FROM Shift;";
        internal const string SqlEmployee = @"SELECT Id, Name FROM Employee;";
    }
}
