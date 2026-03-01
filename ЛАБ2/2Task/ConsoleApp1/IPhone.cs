namespace ConsoleApp1
{
    public class IPhoneLaptop : ILaptop
    {
        public string GetInfo() => "IPhone Laptop: Ultra slim and powerful.";
    }

    public class IPhoneSmartphone : ISmartphone
    {
        public string GetInfo() => "IPhone Smartphone: The ecosystem king.";
    }

    public class IPhoneFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new IPhoneLaptop();
        public ISmartphone CreateSmartphone() => new IPhoneSmartphone();
    }
}