using System;
using System.IO;

namespace AdapterPatternLab
{
    public interface ILogger
    {
        void Log(string message);
        void Error(string message);
        void Warn(string message);
    }
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[LOG]: {message}");
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {message}");
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN]: {message}");
            Console.ResetColor();
        }
    }

    public class FileWriter
    {
        private string _filePath;

        public FileWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void Write(string text)
        {
            File.AppendAllText(_filePath, text);
        }

        public void WriteLine(string text)
        {
            File.AppendAllText(_filePath, text + Environment.NewLine);
        }
    }

    public class FileLoggerAdapter : ILogger
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(FileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public void Log(string message)
        {
            _fileWriter.WriteLine($"[FILE-LOG] {DateTime.Now}: {message}");
        }

        public void Error(string message)
        {
            _fileWriter.WriteLine($"[FILE-ERROR] {DateTime.Now}: {message}");
        }

        public void Warn(string message)
        {
            _fileWriter.WriteLine($"[FILE-WARN] {DateTime.Now}: {message}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine(" Робота зі звичайним Logger ");
            ILogger consoleLogger = new Logger();
            consoleLogger.Log("Система працює стабільно.");
            consoleLogger.Warn("Увага: низький рівень заряду.");
            consoleLogger.Error("Помилка: з'єднання розірвано!");

            Console.WriteLine("\n Робота з FileLoggerчерез Адаптер ");

            FileWriter writer = new FileWriter("log_output.txt");

            ILogger fileLogger = new FileLoggerAdapter(writer);

            fileLogger.Log("Запис успішної операції у файл.");
            fileLogger.Warn("Запис попередження у файл.");
            fileLogger.Error("Запис критичної помилки у файл.");

            Console.WriteLine("\nГотово! Перевірте файл 'log_output.txt' у папці з програмою.");
            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}