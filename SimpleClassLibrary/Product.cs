using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClassLibrary
{
    internal class Product
    {
        protected string name;
        protected double price;
        protected Currency cost;
        protected int quantity;
        protected string producer;
        protected double weight;
        protected int shelfLifeDays;  

        public Product()
        {
            name = "Безіменний товар";
            price = 0.0;
            cost = new Currency();
            quantity = 0;
            producer = "Невідомий виробник";
            weight = 0.0;
            shelfLifeDays = 0;
        }

        public Product(string name, double price, Currency cost, int quantity,
                      string producer, double weight, int shelfLifeDays)
        {
            this.name = name;
            this.price = price;
            this.cost = new Currency(cost);
            this.quantity = quantity;
            this.producer = producer;
            this.weight = weight;
            this.shelfLifeDays = shelfLifeDays;
        }

        public int ShelfLifeDays
        {
            get { return shelfLifeDays; }
            set { shelfLifeDays = value >= 0 ? value : 0; }
        }

        public double ShelfLifeMonths
        {
            get { return shelfLifeDays / 30.0; }
            set { shelfLifeDays = (int)(value * 30); }
        }

        public double ShelfLifeYears
        {
            get { return shelfLifeDays / 365.0; }
            set { shelfLifeDays = (int)(value * 365); }
        }
        public override string ToString()
        {
            return $"Назва: {name}, Ціна: {price} {cost.Name}, " +
                   $"Кількість: {quantity}, Термін придатності: {shelfLifeDays} днів ({ShelfLifeMonths:F2} міс.)";
        }
    }
}
