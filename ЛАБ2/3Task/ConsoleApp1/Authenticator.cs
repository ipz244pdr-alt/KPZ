using System;

namespace ConsoleApp1
{
    public sealed class Authenticator
    {
        private static Authenticator _instance;
        private static readonly object _lock = new object();
        private Authenticator()
        {
            Console.WriteLine("Authenticator: Об'єкт створено (єдиний раз).");
        }
        public static Authenticator GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Authenticator();
                    }
                }
            }
            return _instance;
        }
        public void AuthenticateUser(string username)
        {
            Console.WriteLine($"[AUTH]: Користувач {username} успішно пройшов перевірку.");
        }
    }
}