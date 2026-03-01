using ConsoleApp1;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            ITechFactory factory = new IPhoneFactory();
            Console.WriteLine(factory.CreateLaptop().GetInfo());
            Console.WriteLine(factory.CreateSmartphone().GetInfo());

            Console.WriteLine(new string('-', 30));

            factory = new XiaomiFactory();
            Console.WriteLine(factory.CreateLaptop().GetInfo());
            Console.WriteLine(factory.CreateSmartphone().GetInfo());
        }
    }
}