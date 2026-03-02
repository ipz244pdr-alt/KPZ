using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Character
    {
        public string Name { get; set; }
        public string Height { get; set; }
        public string Build { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public List<string> Inventory { get; set; } = new List<string>();
        public List<string> Deeds { get; set; } = new List<string>();
        public override string ToString()
        {
            string inventory = string.Join(", ", Inventory);
            string deeds = string.Join(", ", Deeds);
            return $"Ім'я: {Name}\nЗріст: {Height}, Статура: {Build}\nВолосся: {HairColor}, Очі: {EyeColor}\n" +
                   $"Інвентар: [{inventory}]\nСправи: [{deeds}]\n";
        }
    }
}