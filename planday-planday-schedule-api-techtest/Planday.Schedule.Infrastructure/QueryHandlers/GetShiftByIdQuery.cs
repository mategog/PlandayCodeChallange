using Dapper;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using Planday.Schedule.Infrastructure.DTOs;
using Planday.Schedule.Infrastructure.Helpers;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Infrastructure.Queries
{
    public class GetShiftByIdQuery : IGetShiftByIdQuery
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly HttpClient _httpClient;

        public GetShiftByIdQuery(IConnectionStringProvider connectionStringProvider, HttpClient httpClient)
        {
            _connectionStringProvider = connectionStringProvider;
            _httpClient = httpClient;
        }
        public async Task<object> QueryByIdAsync(int id)
        {
            await using var sqlConnection = new SqliteConnection(_connectionStringProvider.GetConnectionString());

            var shiftDtos = await sqlConnection.QueryAsync<ShiftDto>(SqlQuery.SqlShift);

            var shift = shiftDtos.Select(x =>
                new Shift(x.Id, x.EmployeeId, DateTime.Parse(x.Start), DateTime.Parse(x.End))).Where(x => x.Id == id).FirstOrDefault();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.Uri}/employee/{shift?.EmployeeId}");
            request.Headers.Add("Authorization", Constants.AuthKey);
            var response = await _httpClient.SendAsync(request);

            var content = JObject.Parse(await response.Content.ReadAsStringAsync());
            var email = content["email"].ToString();

            if (shift != null && response.IsSuccessStatusCode)
            {
                return new { shift, email };
            }
            else
            {
                Console.Error.WriteLine("Shift is empty");
                throw new NullReferenceException("Shift is empty");
            }
        }
    }
}
