namespace Planday.Schedule.Commands
{
    public interface ICreateOpenShift
    {
        Task<bool> CreateOpenShiftAsync<T>(T shift);
    }
}
