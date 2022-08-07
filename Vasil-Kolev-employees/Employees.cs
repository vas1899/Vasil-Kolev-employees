namespace Vasil_Kolev_employees
{
    public class Employees
    {
        public static List<PairEmployeesTimeSpentTogetherOnProject> GetPairEmployeesTimeSpentTogetherOnProjectList(List<EmployeeWorkProject> list)
        {
            List<PairEmployeesTimeSpentTogetherOnProject> employeesTimeSpentOnProjectList = new List<PairEmployeesTimeSpentTogetherOnProject>();
            int length = list.Count;
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (list[i].EmpID != list[j].EmpID && list[i].ProjectID == list[j].ProjectID)
                    {
                        TimeSpan timeSpentTogether = CalculateTimeSpentTogetherOnProject(list[i].DateFrom, list[i].DateTo, list[j].DateFrom, list[j].DateTo);
                        if (timeSpentTogether > TimeSpan.Zero)
                        {
                            // Search for index of the pair of employees that have already worked together on the same project
                            int employeesTimeSpentOnProjectIndex = employeesTimeSpentOnProjectList.FindIndex(a =>
                            (a.EmpId1 == list[i].EmpID && a.EmpId2 == list[j].EmpID)
                            || (a.EmpId2 == list[i].EmpID && a.EmpId1 == list[j].EmpID));

                            if (employeesTimeSpentOnProjectIndex < 0)
                            {
                                PairEmployeesTimeSpentTogetherOnProject pairEmployeesTimeSpentTogetherOnProject =
                                    new PairEmployeesTimeSpentTogetherOnProject
                                    {
                                        EmpId1 = list[i].EmpID,
                                        EmpId2 = list[j].EmpID,
                                        ProjectId = list[i].ProjectID,
                                    };
                                pairEmployeesTimeSpentTogetherOnProject.TimeSpent = timeSpentTogether;
                                employeesTimeSpentOnProjectList.Add(pairEmployeesTimeSpentTogetherOnProject);
                            }
                            else
                            {
                                employeesTimeSpentOnProjectList.ElementAt(employeesTimeSpentOnProjectIndex).TimeSpent += timeSpentTogether;
                            }
                        }
                    }
                }
            }

            return employeesTimeSpentOnProjectList;

        }

        private static TimeSpan CalculateTimeSpentTogetherOnProject(DateTime dateFromEmployee1, DateTime dateToEmployee1, DateTime dateFromEmployee2, DateTime dateToEmployee2)
        {
            DateTime togetherDateFrom;
            DateTime togetherDateTo;
            if (dateFromEmployee1 < dateFromEmployee2)
            {
                togetherDateFrom = dateFromEmployee2;
            }
            else
            {
                togetherDateFrom = dateFromEmployee1;
            }
            if (dateToEmployee1 > dateToEmployee2)
            {
                togetherDateTo = dateToEmployee2;
            }
            else
            {
                togetherDateTo = dateToEmployee1;
            }

            TimeSpan timeSpentTogether = togetherDateTo - togetherDateFrom;
            if (timeSpentTogether < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }
            return timeSpentTogether;
        }


    }
}
