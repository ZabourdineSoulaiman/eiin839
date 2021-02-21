using System;

namespace ExeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
                Console.WriteLine(args[0]);
                Console.WriteLine("Hello World !");
            else
                Console.WriteLine("ExeTest <string parameter>");
        }
    }
}
