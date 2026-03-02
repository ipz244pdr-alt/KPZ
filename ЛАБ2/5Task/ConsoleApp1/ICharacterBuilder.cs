namespace ConsoleApp1
{
    public interface ICharacterBuilder
    {
        ICharacterBuilder SetName(string name);
        ICharacterBuilder SetAppearance(string height, string build, string hair, string eyes);
        ICharacterBuilder AddEquipment(string item);
        Character Build();
    }
}