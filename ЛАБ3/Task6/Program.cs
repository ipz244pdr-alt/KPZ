using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FlyweightPatternLightHTML
{
    public abstract class LightNode
    {
        public abstract string OuterHTML();
    }
    public class LightTextNode : LightNode
    {
        private readonly string _text;
        public LightTextNode(string text) => _text = text;
        public override string OuterHTML() => _text;
    }
    public class TagInfo
    {
        public string TagName { get; }
        public string DisplayType { get; }
        public bool IsSingle { get; }

        public TagInfo(string name, string display, bool single)
        {
            TagName = name;
            DisplayType = display;
            IsSingle = single;
        }
    }
    public class TagFactory
    {
        private static readonly Dictionary<string, TagInfo> _tags = new Dictionary<string, TagInfo>();

        public static TagInfo GetTag(string name, string display, bool single)
        {
            string key = $"{name}_{display}_{single}";
            if (!_tags.ContainsKey(key))
            {
                _tags[key] = new TagInfo(name, display, single);
            }
            return _tags[key];
        }

        public static int TotalTagsCreated => _tags.Count;
    }

    public class LightElementNode : LightNode
    {
        private readonly TagInfo _tagInfo;
        private readonly List<LightNode> _children = new List<LightNode>();
        public LightElementNode(TagInfo info) => _tagInfo = info;

        public void AddChild(LightNode node) => _children.Add(node);

        public override string OuterHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<{_tagInfo.TagName}>");
            foreach (var child in _children) sb.Append(child.OuterHTML());
            if (!_tagInfo.IsSingle) sb.Append($"</{_tagInfo.TagName}>");

            return _tagInfo.DisplayType == "Block" ? sb.ToString() + "\n" : sb.ToString();
        }
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string url = "https://www.gutenberg.org/cache/epub/1513/pg1513.txt";

            Console.WriteLine("Завантаження тексту книги...");
            using var client = new HttpClient();
            string fullText = await client.GetStringAsync(url);
            string[] lines = fullText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            long memoryBefore = GC.GetTotalMemory(true);
            var root = new LightElementNode(TagFactory.GetTag("div", "Block", false));

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line)) continue;

                TagInfo currentTag;

                if (i == 0) 
                    currentTag = TagFactory.GetTag("h1", "Block", false);
                else if (line.Length < 20) 
                    currentTag = TagFactory.GetTag("h2", "Block", false);
                else if (line.StartsWith(" ")) 
                    currentTag = TagFactory.GetTag("blockquote", "Block", false);
                else 
                    currentTag = TagFactory.GetTag("p", "Block", false);

                var element = new LightElementNode(currentTag);
                element.AddChild(new LightTextNode(line));
                root.AddChild(element);
            }

            long memoryAfter = GC.GetTotalMemory(true);

            Console.WriteLine("\n=== Результати верстки (перші 10 елементів) ===\n");
            var preview = root.OuterHTML().Split('\n').Take(15);
            foreach (var p in preview) Console.WriteLine(p);

            Console.WriteLine("\n--- Статистика пам'яті ---");
            Console.WriteLine($"Рядків оброблено: {lines.Length}");
            Console.WriteLine($"Створено унікальних об'єктів TagInfo (Flyweights): {TagFactory.TotalTagsCreated}");
            Console.WriteLine($"Використано пам'яті для всього дерева: {(memoryAfter - memoryBefore) / 1024.0:F2} KB");
            Console.ReadKey();
        }
    }
}