using Dapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Moq;
using Moq.Dapper;
using NUnit.Framework;
using Planday.Schedule.Infrastructure.Providers;
using Planday.Schedule.Infrastructure.Providers.Interfaces;
using Planday.Schedule.Infrastructure.Queries;
using Planday.Schedule.Queries;
using Planday.UnitTests.TestData;
using System.Data;
using System.Data.Common;
using Xunit;

namespace Planday.UnitTests.QueryTest
{
    public class GetAllShiftsQueryTests
    {

        [Fact]
        public async Task QueryAsync_ReturnsShifts_WhenDatabaseHasData()
        {
            // Arrange
            SQLitePCL.Batteries_V2.Init();
            var connectionStringProvider = new Mock<IConnectionStringProvider>();
            connectionStringProvider.Setup(x => x.GetConnectionString()).Returns("Data Source=:memory:");
            var connection = new Mock<SqliteConnection>();
            connection.Object.Open();
            connection.Object.Execute("CREATE TABLE Shift (Id INT, EmployeeId INT, Start TEXT, End TEXT)");

            var query = new GetAllShiftsQuery(connectionStringProvider.Object);

            var expected = new[] { 7, 77, 777 };
            var expected2 = TestDataFactory.CreateTestShifts();

            connection.Setup(c => c.QueryAsync<TestShift>(It.IsAny<string>(), null, null, null, null))
                      .ReturnsAsync(expected2);

            // Act
            var result = await query.QueryAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task QueryAsync_ReturnsShifts_WhenDatabaseHasData2()
        {
            // Create a real in-memory SQLite connection
            using var connection = new SqliteConnection("Data Source=:memory:");
            await connection.OpenAsync();

            // Create the 'Shift' table in the real SQLite database
            await connection.ExecuteAsync("CREATE TABLE Shift (Id INT, EmployeeId INT, Start TEXT, End TEXT)");

            // Now you can use your query with the real connection
            var connectionStringProvider = new Mock<IConnectionStringProvider>();
            connectionStringProvider.Setup(x => x.GetConnectionString()).Returns(connection.ConnectionString);

            var query = new GetAllShiftsQuery(connectionStringProvider.Object);

            // Continue with the rest of your test as before
            var expected2 = TestDataFactory.CreateTestShifts();

            // Insert test data into the 'Shift' table
            await connection.ExecuteAsync("INSERT INTO Shift (Id, EmployeeId, Start, End) VALUES (@Id, @EmployeeId, @Start, @End)", expected2);

            // Act
            var result = await query.QueryAsync();

            // Assert
            result.Should().BeEquivalentTo(expected2);
        }
    }
}
