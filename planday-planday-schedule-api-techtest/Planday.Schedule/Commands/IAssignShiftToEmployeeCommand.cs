namespace Planday.Schedule.Commands
{
    public interface IAssignShiftToEmployeeCommand
    {
        Task<int> AssignShiftToEmployee(int shiftId, int employeeId);
    }
}
