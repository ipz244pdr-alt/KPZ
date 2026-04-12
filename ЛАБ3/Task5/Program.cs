using System;
using System.Collections.Generic;
using System.Text;

namespace CompositePatternLightHTML
{
    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
    }
    public class LightTextNode : LightNode
    {
        private readonly string _text;
        public LightTextNode(string text) => _text = text;

        public override string InnerHTML() => _text;
        public override string OuterHTML() => _text;
    }
    public enum DisplayType { Block, Inline }
    public enum ClosingType { Normal, Single }

    public class LightElementNode : LightNode
    {
        private readonly string _tagName;
        private readonly DisplayType _displayType;
        private readonly ClosingType _closingType;
        private readonly List<string> _cssClasses = new List<string>();
        private readonly List<LightNode> _children = new List<LightNode>();
        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            _tagName = tagName;
            _displayType = display;
            _closingType = closing;
        }

        public void AddClass(string className) => _cssClasses.Add(className);
        public void AddChild(LightNode node) => _children.Add(node);

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children)
            {
                sb.Append(child.OuterHTML());
            }
            return sb.ToString();
        }
        public override string OuterHTML()
        {
            StringBuilder sb = new StringBuilder();
            string classes = _cssClasses.Count > 0 ? $" class=\"{string.Join(" ", _cssClasses)}\"" : "";

            sb.Append($"<{_tagName}{classes}");

            if (_closingType == ClosingType.Single)
            {
                sb.Append(" />");
            }
            else
            {
                sb.Append(">");
                sb.Append(InnerHTML());
                sb.Append($"</{_tagName}>");
            }
            return _displayType == DisplayType.Block ? sb.ToString() + Environment.NewLine : sb.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var table = new LightElementNode("table", DisplayType.Block, ClosingType.Normal);
            table.AddClass("main-table");

            var headerRow = new LightElementNode("tr", DisplayType.Block, ClosingType.Normal);

            var th1 = new LightElementNode("th", DisplayType.Inline, ClosingType.Normal);
            th1.AddChild(new LightTextNode("Назва"));

            var th2 = new LightElementNode("th", DisplayType.Inline, ClosingType.Normal);
            th2.AddChild(new LightTextNode("Ціна"));

            headerRow.AddChild(th1);
            headerRow.AddChild(th2);
            table.AddChild(headerRow);
            var dataRow = new LightElementNode("tr", DisplayType.Block, ClosingType.Normal);

            var td1 = new LightElementNode("td", DisplayType.Inline, ClosingType.Normal);
            td1.AddChild(new LightTextNode("Ноутбук"));

            var td2 = new LightElementNode("td", DisplayType.Inline, ClosingType.Normal);
            td2.AddChild(new LightTextNode("25000 грн"));

            dataRow.AddChild(td1);
            dataRow.AddChild(td2);
            table.AddChild(dataRow);

            var hr = new LightElementNode("hr", DisplayType.Block, ClosingType.Single);
            Console.WriteLine("=== Вивід LightHTML Таблиці ===\n");
            Console.WriteLine(table.OuterHTML());
            Console.WriteLine(hr.OuterHTML());

            Console.WriteLine("\n=== Перевірка InnerHTML таблиці (тільки вміст) ===");
            Console.WriteLine(table.InnerHTML());
            Console.ReadKey();
        }
    }
}
