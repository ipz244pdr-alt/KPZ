using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CompositePatternLightHTML
{
    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();

        public string Render()
        {
            OnBeforeRender();
            string html = OuterHTML();
            OnAfterRender();
            return html;
        }

        protected virtual void OnBeforeRender() { }
        protected virtual void OnAfterRender() { }
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

    public class LightElementNode : LightNode, IEnumerable<LightNode>
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
        public List<LightNode> GetChildren() => _children;

        protected override void OnBeforeRender()
        {
            Console.WriteLine($"Starting render of <{_tagName}>");
        }

        protected override void OnAfterRender()
        {
            Console.WriteLine($"Finished render of <{_tagName}>");
        }

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children)
            {
                sb.Append(child.Render());
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

        public IEnumerator<LightNode> GetEnumerator() => new LightElementEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class LightElementEnumerator : IEnumerator<LightNode>
    {
        private readonly List<LightNode> _flatList = new List<LightNode>();
        private int _currentIndex = -1;

        public LightElementEnumerator(LightNode root) => Flatten(root);

        private void Flatten(LightNode node)
        {
            _flatList.Add(node);
            if (node is LightElementNode element)
            {
                foreach (var child in element.GetChildren())
                {
                    Flatten(child);
                }
            }
        }

        public LightNode Current => _flatList[_currentIndex];
        object IEnumerator.Current => Current;
        public bool MoveNext() => ++_currentIndex < _flatList.Count;
        public void Reset() => _currentIndex = -1;
        public void Dispose() { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var table = new LightElementNode("table", DisplayType.Block, ClosingType.Normal);
            var tr = new LightElementNode("tr", DisplayType.Block, ClosingType.Normal);
            var td = new LightElementNode("td", DisplayType.Inline, ClosingType.Normal);
            td.AddChild(new LightTextNode("Cell content"));
            tr.AddChild(td);
            table.AddChild(tr);

            Console.WriteLine("=== DFS Iterator Test ===");
            foreach (var node in table)
            {
                Console.WriteLine($"Node: {node.GetType().Name}");
            }
        }
    }
}
