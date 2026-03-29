using System;

namespace DecoratorPatternRPG
{
    public interface IHero
    {
        string GetDescription();
        int GetDamage();
        int GetArmor();
    }
    public class Warrior : IHero
    {
        public string GetDescription() => "Воїн";
        public int GetDamage() => 15;
        public int GetArmor() => 10;
    }
    public class Mage : IHero
    {
        public string GetDescription() => "Маг";
        public int GetDamage() => 25;
        public int GetArmor() => 2;
    }
    public class Paladin : IHero
    {
        public string GetDescription() => "Паладин";
        public int GetDamage() => 12;
        public int GetArmor() => 15;
    }
    public abstract class InventoryDecorator : IHero
    {
        protected IHero _hero;

        public InventoryDecorator(IHero hero)
        {
            _hero = hero;
        }

        public virtual string GetDescription() => _hero.GetDescription();
        public virtual int GetDamage() => _hero.GetDamage();
        public virtual int GetArmor() => _hero.GetArmor();
    }
    public class SwordDecorator : InventoryDecorator
    {
        public SwordDecorator(IHero hero) : base(hero) { }

        public override string GetDescription() => _hero.GetDescription() + " з Мечем";
        public override int GetDamage() => _hero.GetDamage() + 10;
    }
    public class PlateArmorDecorator : InventoryDecorator
    {
        public PlateArmorDecorator(IHero hero) : base(hero) { }

        public override string GetDescription() => _hero.GetDescription() + ", в Латних обладунках";
        public override int GetArmor() => _hero.GetArmor() + 20;
    }
    public class RingOfPowerDecorator : InventoryDecorator
    {
        public RingOfPowerDecorator(IHero hero) : base(hero) { }

        public override string GetDescription() => _hero.GetDescription() + " та Кільцем Сили";
        public override int GetDamage() => _hero.GetDamage() + 5;
        public override int GetArmor() => _hero.GetArmor() + 5;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IHero myHero = new Mage();
            Console.WriteLine("--- Початковий герой ---");
            PrintStats(myHero);

            myHero = new PlateArmorDecorator(myHero);

            myHero = new SwordDecorator(myHero);

            myHero = new RingOfPowerDecorator(myHero);

            Console.WriteLine("\n--- Герой після екіпірування (багатошаровий декоратор) ---");
            PrintStats(myHero);
            IHero dualSwordWarrior = new Warrior();
            dualSwordWarrior = new SwordDecorator(dualSwordWarrior);
            dualSwordWarrior = new SwordDecorator(dualSwordWarrior);

            Console.WriteLine("\n--- Воїн з двома мечами ---");
            PrintStats(dualSwordWarrior);

            Console.ReadKey();
        }
        static void PrintStats(IHero hero)
        {
            Console.WriteLine($"Опис: {hero.GetDescription()}");
            Console.WriteLine($"Шкода (Damage): {hero.GetDamage()}");
            Console.WriteLine($"Захист (Armor): {hero.GetArmor()}");
        }
    }
}