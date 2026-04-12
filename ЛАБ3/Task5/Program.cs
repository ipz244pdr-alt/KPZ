using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace CompositePatternLightHTML
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public class AddClassCommand : ICommand
    {
        private readonly LightElementNode _node;
        private readonly string _className;

        public AddClassCommand(LightElementNode node, string className)
        {
            _node = node;
            _className = className;
        }

        public void Execute() => _node.AddClass(_className);
        public void Undo() => _node.RemoveClass(_className);
    }

    public interface ILightNodeVisitor
    {
        void Visit(LightTextNode textNode);
        void Visit(LightElementNode elementNode);
    }

    public interface IElementState
    {
        string Render(LightElementNode node);
    }

    public class VisibleState : IElementState
    {
        public string Render(LightElementNode node) => node.BaseOuterHTML();
    }

    public class HiddenState : IElementState
    {
        public string Render(LightElementNode node) => "";
    }

    public abstract class LightNode
    {
        public abstract string OuterHTML();
        public abstract string InnerHTML();
        public abstract void Accept(ILightNodeVisitor visitor);

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
        public override void Accept(ILightNodeVisitor visitor) => visitor.Visit(this);
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
        private IElementState _state = new VisibleState();

        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            _tagName = tagName;
            _displayType = display;
            _closingType = closing;
        }

        public void SetState(IElementState state) => _state = state;
        public void AddClass(string className) => _cssClasses.Add(className);
        public void RemoveClass(string className) => _cssClasses.Remove(className);
        public void AddChild(LightNode node) => _children.Add(node);
        public List<LightNode> GetChildren() => _children;

        public override void Accept(ILightNodeVisitor visitor)
        {
            visitor.Visit(this);
            foreach (var child in _children) child.Accept(visitor);
        }

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var child in _children) sb.Append(child.Render());
            return sb.ToString();
        }

        public override string OuterHTML() => _state.Render(this);

        public string BaseOuterHTML()
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
                foreach (var child in element.GetChildren()) Flatten(child);
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
            var p = new LightElementNode("p", DisplayType.Block, ClosingType.Normal);
            p.AddChild(new LightTextNode("Testing Command Pattern"));

            var addClass = new AddClassCommand(p, "highlight-text");

            Console.WriteLine("Executing Command...");
            addClass.Execute();
            Console.WriteLine(p.Render());

            Console.WriteLine("Undoing Command...");
            addClass.Undo();
            Console.WriteLine(p.Render());
        }
    }
}
