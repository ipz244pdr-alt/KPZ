using System;

namespace BridgePatternLab
{
    public interface IRenderer
    {
        void Render(string shapeType);
    }

    public class VectorRenderer : IRenderer
    {
        public void Render(string shapeType)
        {
            Console.WriteLine($"Drawing {shapeType} as lines (Vector graphics).");
        }
    }
    public class RasterRenderer : IRenderer
    {
        public void Render(string shapeType)
        {
            Console.WriteLine($"Drawing {shapeType} as pixels (Raster graphics).");
        }
    }
    public abstract class Shape
    {
        protected IRenderer _renderer;
        protected string _name;

        protected Shape(IRenderer renderer, string name)
        {
            _renderer = renderer;
            _name = name;
        }
        public virtual void Draw()
        {
            _renderer.Render(_name);
        }
    }
    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer, "Circle") { }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer, "Square") { }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer, "Triangle") { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IRenderer vectorTool = new VectorRenderer();
            IRenderer rasterTool = new RasterRenderer();

            Shape redCircle = new Circle(vectorTool);
            Shape blueSquare = new Square(rasterTool);
            Shape greenTriangle = new Triangle(vectorTool);
            Shape yellowTriangle = new Triangle(rasterTool);

            Console.WriteLine("=== Графічний редактор (Шаблон Міст) ===\n");
            redCircle.Draw();
            blueSquare.Draw();
            greenTriangle.Draw();
            yellowTriangle.Draw();

            Console.WriteLine("\nДемонстрація завершена. Натисніть будь-яку клавішу...");
            Console.ReadKey();
        }
    }
}