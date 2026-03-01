namespace ConsoleApp1
{
    public class XiaomiLaptop : ILaptop
    {
        public string GetInfo() => "Xiaomi Laptop: Best price/performance ratio.";
    }

    public class XiaomiSmartphone : ISmartphone
    {
        public string GetInfo() => "Xiaomi Smartphone: High specs for everyone.";
    }

    public class XiaomiFactory : ITechFactory
    {
        public ILaptop CreateLaptop() => new XiaomiLaptop();
        public ISmartphone CreateSmartphone() => new XiaomiSmartphone();
    }
}