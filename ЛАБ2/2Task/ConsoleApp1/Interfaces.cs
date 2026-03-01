namespace ConsoleApp1
{
    public interface ILaptop { string GetInfo(); }
    public interface ISmartphone { string GetInfo(); }

    public interface ITechFactory
    {
        ILaptop CreateLaptop();
        ISmartphone CreateSmartphone();
    }
}