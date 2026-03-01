using System;
using System.Collections.Generic;

namespace ConsoleApp1
{

    public abstract class Subscription
    {
        public abstract decimal MonthlyFee { get; }
        public abstract int MinimumPeriodMonths { get; }
        public abstract List<string> Channels { get; }

        public void PrintDetails()
        {
            Console.WriteLine($"Тип: {this.GetType().Name}");
            Console.WriteLine($"Ціна: {MonthlyFee} грн/міс");
            Console.WriteLine($"Мін. період: {MinimumPeriodMonths} міс.");
            Console.WriteLine($"Канали: {string.Join(", ", Channels)}\n");
        }
    }


    public class DomesticSubscription : Subscription
    {
        public override decimal MonthlyFee => 150.00m;
        public override int MinimumPeriodMonths => 1;
        public override List<string> Channels => new List<string> { "1+1", "Inter", "Новий Канал" };
    }

    public class EducationalSubscription : Subscription
    {
        public override decimal MonthlyFee => 80.00m;
        public override int MinimumPeriodMonths => 3;
        public override List<string> Channels => new List<string> { "Discovery", "Science" };
    }

    public class PremiumSubscription : Subscription
    {
        public override decimal MonthlyFee => 450.00m;
        public override int MinimumPeriodMonths => 1;
        public override List<string> Channels => new List<string> { "HBO", "Netflix", "4K Sport" };
    }
}