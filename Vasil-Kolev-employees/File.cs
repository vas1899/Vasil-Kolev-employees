using System.Globalization;

namespace Vasil_Kolev_employees
{
    public class File
    {
        public static FileInfo[] GetFilesInDirectory(string path, string format)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles(format);

            return files;
        }

        public static List<EmployeeWorkProject> ExtractDataFromCSVFile(string fileName, string path)
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
        private static DateTime GetDateTimeDateTo(string dateTo)
        {
            if (!string.IsNullOrEmpty(dateTo) && !dateTo.Contains("NULL"))
            {
                return DateTime.Parse(dateTo, CultureInfo.CurrentCulture);

            }
            return DateTime.Today;
        }
    }
}
