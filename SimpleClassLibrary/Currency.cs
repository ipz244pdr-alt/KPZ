using System;

namespace SimpleClassLibrary
{
    public class Currency
    {
        protected string name;
        protected double exRate;

        public Currency()
        {
            name = "UAH";
            exRate = 1.0;
        }

        public Currency(string name, double exRate)
        {
            this.name = name;
            this.exRate = exRate;
        }

        public Currency(Currency other)
        {
            this.name = other.name;
            this.exRate = other.exRate;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public double ExRate
        {
            get { return exRate; }
            set { exRate = value; }
        }

        public override string ToString()
        {
            return $"{name} (Курс: {exRate})";
        }

    }
}
