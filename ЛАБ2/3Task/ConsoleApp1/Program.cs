using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Перевірка роботи Одинака ===");

            Authenticator auth1 = Authenticator.GetInstance();
            auth1.AuthenticateUser("Admin_1");

            Authenticator auth2 = Authenticator.GetInstance();
            auth2.AuthenticateUser("Admin_2");

            if (ReferenceEquals(auth1, auth2))
            {
                Console.WriteLine("\nРЕЗУЛЬТАТ: auth1 та auth2 — це один і той самий екземпляр.");
                Console.WriteLine("Принцип Singleton дотримано.");
            }
            else
            {
                Console.WriteLine("\nПОМИЛКА: Створено різні екземпляри!");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}