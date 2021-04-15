using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFirstSOAPMaths
{
    class Program
    {
        static void Main(string[] args)
        {
            MyMaths.MathsOperationsClient mathClient = new MyMaths.MathsOperationsClient();
            int result = mathClient.Add(5, 100);
            Console.WriteLine("result : " + result);
            Console.ReadLine();
        }
    }
}
