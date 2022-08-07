using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class PairEmployeesTimeSpentTogetherOnProject
    {
        public int EmpId1 { get; set; }
        public int EmpId2 { get; set; }
        public int ProjectId { get; set; }
        public TimeSpan TimeSpent { get; set; }

    }
}
