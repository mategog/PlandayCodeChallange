namespace Planday.Schedule.Queries
{
    public interface IGetShiftByIdQuery
    {
        Task<object> QueryByIdAsync(int id);
    }
}
