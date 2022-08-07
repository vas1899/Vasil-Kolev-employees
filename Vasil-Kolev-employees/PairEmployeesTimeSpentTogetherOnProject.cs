namespace Vasil_Kolev_employees
{
    public class PairEmployeesTimeSpentTogetherOnProject
    {
        public int EmpId1 { get; set; }
        public int EmpId2 { get; set; }
        public int ProjectId { get; set; }
        public TimeSpan TimeSpent { get; set; }

    }
}
