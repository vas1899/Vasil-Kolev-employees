using System.Globalization;

namespace Vasil_Kolev_employees
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Data"));
            string format = "*.csv";
            Console.WriteLine("Local Date Format is" + CultureInfo.CurrentCulture);

            Console.WriteLine($"In Directory {path}\nFiles:");

            FileInfo[] csvFiles = File.GetFilesInDirectory(path, format);
            ConsolePrinter.PrintAllFiles(csvFiles);

            int chosenFileIndex = ConsolePrinter.AskUserForInput(csvFiles.Length);


            string fileName = csvFiles[chosenFileIndex].Name;

            List<EmployeeWorkProject> employeeWorkonProjectlist = File.ExtractDataFromCSVFile(fileName, path);


            List<PairEmployeesTimeSpentTogetherOnProject> employeesTimeSpentOnProjectList = Employees.GetPairEmployeesTimeSpentTogetherOnProjectList(employeeWorkonProjectlist);
            ConsolePrinter.PrintListPairEmployeesTimeSpentOnProjectList(employeesTimeSpentOnProjectList);
        }
    }
}