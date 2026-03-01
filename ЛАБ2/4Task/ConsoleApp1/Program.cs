using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Virus grandParent = new Virus(10.5, 50, "Alfa-Virus", "Root");

            Virus parent1 = new Virus(5.2, 20, "Beta-1", "Infector");
            Virus parent2 = new Virus(4.8, 18, "Beta-2", "Trojan");
            grandParent.Children.Add(parent1);
            grandParent.Children.Add(parent2);

            parent1.Children.Add(new Virus(1.1, 2, "Gamma-Sub1", "Small"));
            parent1.Children.Add(new Virus(0.9, 1, "Gamma-Sub2", "Small"));

            Console.WriteLine("--- Оригінальна сім'я вірусів ---");
            grandParent.ShowInfo();

            Virus clonedFamily = (Virus)grandParent.Clone();

            Console.WriteLine("\n--- Клонована сім'я вірусів ---");
            clonedFamily.ShowInfo();

            Console.WriteLine("\n--- Перевірка глибокого копіювання ---");
            bool isSameReference = ReferenceEquals(grandParent, clonedFamily);
            bool isFirstChildSame = ReferenceEquals(grandParent.Children[0], clonedFamily.Children[0]);

            Console.WriteLine($"Батьки - це різні об'єкти в пам'яті? {!isSameReference}");
            Console.WriteLine($"Діти - це різні об'єкти в пам'яті? {!isFirstChildSame}");

            Console.ReadKey();
        }
    }
}