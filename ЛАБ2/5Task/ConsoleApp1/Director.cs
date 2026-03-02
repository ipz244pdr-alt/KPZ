namespace ConsoleApp1
{
    public class Director
    {
        public void ConstructStandardWarrior(ICharacterBuilder builder)
        {
            builder.SetName("Стандартний воїн")
                   .SetAppearance("180", "Середня", "Коричневе", "Сірі")
                   .AddEquipment("Базовий набір спорядження");
        }
    }
}