global using System; // global using ...
using System.Runtime.CompilerServices;

namespace CSharp10;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World from C# 10.0!");

        var sean = new Foo { Name = "Sean" };
        var bob = new Foo { Name = "Bob" };
        Console.WriteLine(sean);
        Console.WriteLine(bob);

        Point p = new(10, 20);
        int x = 0;
        (x, int y) = p; // <-- we can combine existing declarations with new ones when destructuring record


        var f = (int a, int b) => a + b;
        int res = Dump(f(10, 20));
    }

    private static int Dump(int a, [CallerArgumentExpression("a")] string aExpr = "")
    {
        Console.WriteLine(aExpr);
        return a;
    }
}

public record Point(int x, int y);

// record structs
public record Foo
{ 
    public string Name { get; set; }

    public sealed override string ToString()
    {
        return $"Foo: {Name}";
    }
}

public record Bar : Foo;