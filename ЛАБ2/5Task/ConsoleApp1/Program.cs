using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            HeroBuilder hBuilder = new HeroBuilder();
            Character hero = hBuilder.SetName("Артур")
                                     .SetAppearance("185см", "Атлетична", "Золотисте", "Блакитні")
                                     .AddEquipment("Екскалібур")
                                     .AddGoodDeed("Врятував село")
                                     .Build();

            EnemyBuilder eBuilder = new EnemyBuilder();
            Character enemy = eBuilder.SetName("Мордред")
                                      .SetAppearance("190см", "Худорлява", "Сиве", "Червоні")
                                      .AddEquipment("Чорна магія")
                                      .AddEvilDeed("Захопив трон")
                                      .Build();

            Console.WriteLine("--- ГЕРОЙ ---\n" + hero);
            Console.WriteLine("--- ВОРОГ ---\n" + enemy);

            Console.ReadKey();
        }
    }
}