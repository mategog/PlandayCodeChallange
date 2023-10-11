using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planday.UnitTests.TestData
{
    public class TestShift
    {
        public int? Id { get; set; }
        public int? EmployeeId { get; set; }
        public string Start { get; set; }
        public string End { get; set; }

        public TestShift(int id, int? employeeId, string start, string end)
        {
            Id = id;
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
    }

    public class TestEmployee
    {
        public int Id { get; }
        public string Name { get; }

        public TestEmployee(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
