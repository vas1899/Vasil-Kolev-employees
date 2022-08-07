using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Data"));
            string format = "*.csv";
            Console.WriteLine($"In Directory {path}\nFiles:");

            FileInfo[] csvFiles = GetFilesInDirectory(path, format);
            PrintAllFiles(csvFiles);

            int chosenFileIndex = GetUserChosenFileIndex();


            string fileName = csvFiles[chosenFileIndex].Name;

            List<EmployeeWorkProject> employeeWorkonProjectlist = ExtractDataFromCSVFile(fileName, path);


            List<PairEmployeesTimeSpentTogetherOnProject> employeesTimeSpentOnProjectList = GetPairEmployeesTimeSpentTogetherOnProjectList(employeeWorkonProjectlist);
            PrintListPairEmployeesTimeSpentOnProjectList(employeesTimeSpentOnProjectList);
        }

        public static void PrintListPairEmployeesTimeSpentOnProjectList(List<PairEmployeesTimeSpentTogetherOnProject> employeesTimeSpentOnProjectList)
        {
            Console.WriteLine("\n\nEmployee ID #1\t Employee ID #2  Project ID\t Days worked");
            foreach (var pairEmployees in employeesTimeSpentOnProjectList)
            {
                Console.WriteLine($"{pairEmployees.EmpId1}\t \t {pairEmployees.EmpId2}\t\t  {pairEmployees.ProjectId}\t\t  {pairEmployees.TimeSpent.Days}");
            }
        }
        private static int GetUserChosenFileIndex()
        {
            int fileNumberInt;
            string fileNumberString;
            do
            {
                Console.WriteLine("\n\nPlease enter the file number:\n");
                fileNumberString = Console.ReadLine() ?? "";

            }
            while (!int.TryParse(fileNumberString, out fileNumberInt));

            return fileNumberInt - 1;
        }

        private static void PrintAllFiles(FileInfo[] files)
        {
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                Console.WriteLine(i + 1 + ". " + files[i].Name);
            }
        }

        private static FileInfo[] GetFilesInDirectory(string path, string format)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles(format);

            return files;
        }

        private static List<EmployeeWorkProject> ExtractDataFromCSVFile(string fileName, string path)
        {
            List<EmployeeWorkProject> employeeWorkonProjectlist = new List<EmployeeWorkProject>();
            string filePath = Path.Combine(path, fileName);
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    var values = line.Split(", ");

                    employeeWorkonProjectlist.Add(new EmployeeWorkProject
                    {
                        EmpID = Convert.ToInt32(values[0]),
                        ProjectID = Convert.ToInt32(values[1]),
                        DateFrom = Convert.ToDateTime(values[2]),
                        DateTo = GetDateTimeDateTo(values[3])
                    });
                }
            }
            return employeeWorkonProjectlist;
        }

        private static List<PairEmployeesTimeSpentTogetherOnProject> GetPairEmployeesTimeSpentTogetherOnProjectList(List<EmployeeWorkProject> list)
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

        private static TimeSpan CalculateTimeSpentTogetherOnProject(DateTime dateFrom1, DateTime dateTo1, DateTime dateFrom2, DateTime dateTo2)
        {
            DateTime togetherDateFrom;
            DateTime togetherDateTo;
            if (dateFrom1 < dateFrom2)
            {
                togetherDateFrom = dateFrom2;
            }
            else
            {
                togetherDateFrom = dateFrom1;
            }
            if (dateTo1 > dateTo2)
            {
                togetherDateTo = dateTo2;
            }
            else
            {
                togetherDateTo = dateTo1;
            }

            TimeSpan timeSpentTogether = togetherDateTo - togetherDateFrom;
            if (timeSpentTogether < TimeSpan.Zero)
            {
                return TimeSpan.Zero;
            }
            return timeSpentTogether;
        }

        private static DateTime GetDateTimeDateTo(string dateTo)
        {
            if (!string.IsNullOrEmpty(dateTo) && !dateTo.Contains("NULL"))
            {
                return Convert.ToDateTime(dateTo);
            }
            return DateTime.Today;
        }

    }
}