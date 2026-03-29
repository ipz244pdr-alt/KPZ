using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ProxyPatternLab
{
    public interface ISmartTextReader
    {
        char[][] ReadText(string filePath);
    }
    public class SmartTextReader : ISmartTextReader
    {
        public char[][] ReadText(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не знайдено.");
                return null;
            }
            string[] lines = File.ReadAllLines(filePath);
            char[][] result = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return result;
        }
    }
    public class SmartTextChecker : ISmartTextReader
    {
        private readonly ISmartTextReader _reader;

        public SmartTextChecker(ISmartTextReader reader)
        {
            _reader = reader;
        }
        public char[][] ReadText(string filePath)
        {
            Console.WriteLine($"[LOG]: Спроба відкриття файлу: {filePath}");
            char[][] result = _reader.ReadText(filePath);
            if (result != null)
            {
                int totalChars = 0;
                foreach (var line in result) totalChars += line.Length;

                Console.WriteLine($"[LOG]: Файл успішно прочитано.");
                Console.WriteLine($"[INFO]: Рядків: {result.Length}, Символів: {totalChars}");
                Console.WriteLine($"[LOG]: Файл закрито.");
            }
            return result;
        }
    }
    public class SmartTextReaderLocker : ISmartTextReader
    {
        private readonly ISmartTextReader _reader;
        private readonly Regex _lockPattern;

        public SmartTextReaderLocker(ISmartTextReader reader, string pattern)
        {
            _reader = reader;
            _lockPattern = new Regex(pattern);
        }

        public char[][] ReadText(string filePath)
        {
            if (_lockPattern.IsMatch(filePath))
            {
                Console.WriteLine($"[SECURITY]: Access denied to file: {filePath}!");
                return null;
            }

            return _reader.ReadText(filePath);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string publicFile = "public.txt";
            string privateFile = "secret_data.txt";
            File.WriteAllText(publicFile, "Hello\nWorld!");
            File.WriteAllText(privateFile, "Top secret information");

            ISmartTextReader reader = new SmartTextReader();

            ISmartTextReader loggedReader = new SmartTextChecker(reader);
            ISmartTextReader secureReader = new SmartTextReaderLocker(loggedReader, ".*secret.*");

            Console.WriteLine("=== Спроба 1: Читання дозволеного файлу ===");
            secureReader.ReadText(publicFile);

            Console.WriteLine("\n=== Спроба 2: Читання забороненого файлу ===");
            secureReader.ReadText(privateFile);
            File.Delete(publicFile);
            File.Delete(privateFile);

            Console.ReadKey();
        }
    }
}