using System;
using SimpleClassLibrary;

namespace SimpleClassConlsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] products = ReadProductsArray();
            PrintProducts(products);
        }

        static Product[] ReadProductsArray()
        {
            Console.Write("Кількість товарів: ");
            int n = int.Parse(Console.ReadLine());
            Product[] products = new Product[n];

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nТовар #{i + 1}:");
                Console.Write("Назва: ");
                string name = Console.ReadLine();

                Console.Write("Ціна: ");
                double price = double.Parse(Console.ReadLine());

                Console.Write("Валюта (напр. USD): ");
                string currencyName = Console.ReadLine();

                Console.Write("Курс валюти до UAH: ");
                double exRate = double.Parse(Console.ReadLine());
                Currency cost = new Currency(currencyName, exRate);

                Console.Write("Кількість: ");
                int quantity = int.Parse(Console.ReadLine());

                Console.Write("Виробник: ");
                string producer = Console.ReadLine();

                Console.Write("Вага (кг): ");
                double weight = double.Parse(Console.ReadLine());

                Console.WriteLine("Одиниці вимірювання терміну придатності:");
                Console.WriteLine("1. Дні");
                Console.WriteLine("2. Місяці");
                Console.WriteLine("3. Роки");
                Console.Write("Ваш вибір: ");
                int choice = int.Parse(Console.ReadLine());

                Console.Write("Термін придатності: ");
                double shelfLifeValue = double.Parse(Console.ReadLine());
                int shelfLifeDays = 0;

               public static int CalculateDays(double value, int unitChoice)
{
    return unitChoice switch
    {
        1 => (int)value,       
        2 => (int)(value * 30),    
        3 => (int)(value * 365),   
        _ => 0
    };
}

                products[i] = new Product(name, price, cost, quantity, producer, weight, shelfLifeDays);
            }
            return products;
        }

        static void PrintProducts(Product[] products)
        {
            foreach (var product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }
    }
}
