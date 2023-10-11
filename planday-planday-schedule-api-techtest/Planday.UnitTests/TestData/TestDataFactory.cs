using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planday.UnitTests.TestData
{
    internal abstract class TestDataFactory
    {
        public static List<TestEmployee> CreateTestEmployees()
        {
            var employees = new List<TestEmployee>();

            for (int i = 1; i <= 5; i++)
            {
                var employee = new TestEmployee(i, $"Employee {i}");
                employees.Add(employee);
            }

            return employees;
        }

        public static List<TestShift> CreateTestShifts()
        {
            var shifts = new List<TestShift>();

            for (int i = 1; i <= 5; i++)
            {
                var start = DateTime.Now.AddHours(i);
                var end = start.AddHours(4);

                var shift = new TestShift(i, i * 100, start.ToString(), end.ToString());
                shifts.Add(shift);
            }

            return shifts;
        }
    }
}
