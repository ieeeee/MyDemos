using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.ClientTestApp.UseServiceReferences.ServiceReference1;

namespace WCF.ClientTestApp.UseServiceReferences
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.StudentServiceClient client = new ServiceReference1.StudentServiceClient();

            IEnumerable<StudentInfo> x = client.GetStudentInfo(Int32.Parse("10010"));

            foreach (StudentInfo s in x)
            {
                Console.WriteLine(s.FirstName + s.LastName);
            }

            Console.ReadKey();
        }
    }
}
