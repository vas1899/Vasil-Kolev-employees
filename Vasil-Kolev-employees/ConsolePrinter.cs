namespace Vasil_Kolev_employees
{
    public class ConsolePrinter
    {
        public static void PrintListPairEmployeesTimeSpentOnProjectList(List<PairEmployeesTimeSpentTogetherOnProject> employeesTimeSpentOnProjectList)
        {
            Console.WriteLine(" {0,14} {1,14} {2,10} {3,11}", "Employee ID #1", "Employee ID #2", "Project ID", "Days worked");
            foreach (var pairEmployees in employeesTimeSpentOnProjectList)
            {
                Console.WriteLine(" {0,14} {1,14} {2,10} {3,11}", pairEmployees.EmpId1, pairEmployees.EmpId2, pairEmployees.ProjectId, pairEmployees.TimeSpent.Days);
            }
        }
        public static void PrintAllFiles(FileInfo[] files)
        {
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(i + 1 + ". " + files[i].Name);
            }
        }

        public static int AskUserForInput(int availableOptions)
        {
            int fileNumberInt;
            string fileNumberString;
            do
            {
                Console.WriteLine("\n\nPlease enter the file number:\n");
                fileNumberString = Console.ReadLine() ?? "";
            }
            while (!int.TryParse(fileNumberString, out fileNumberInt) || fileNumberInt > availableOptions || fileNumberInt <= 0);

            return fileNumberInt - 1;
        }
    }
}
