using System;
using System.Collections.Generic;
using ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine(" СИСТЕМА УПРАВЛІННЯ ПІДПИСКАМИ \n");

        var channels = new List<SubscriptionPurchasing>
        {
            new WebSite(),
            new MobileApp(),
            new ManagerCall()
        };

        foreach (var channel in channels)
        {
            channel.FinalizePurchase();
        }

        Console.WriteLine("Програма завершена. Натисніть будь-яку клавішу...");
        Console.ReadKey();
    }
}