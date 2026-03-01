using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Virus : ICloneable
    {
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public List<Virus> Children { get; set; }

        public Virus(double weight, int age, string name, string type)
        {
            Weight = weight;
            Age = age;
            Name = name;
            Type = type;
            Children = new List<Virus>();
        }

        public object Clone()
        {
            Virus clone = new Virus(this.Weight, this.Age, this.Name, this.Type);

            foreach (var child in this.Children)
            {
                clone.Children.Add((Virus)child.Clone());
            }

            return clone;
        }

        public void ShowInfo(int indent = 0)
        {
            string space = new string(' ', indent * 4);
            Console.WriteLine($"{space}Вірус: {Name} (Тип: {Type}, Вік: {Age}, Вага: {Weight})");
            foreach (var child in Children)
            {
                child.ShowInfo(indent + 1);
            }
        }
    }
}