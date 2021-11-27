using System;

// there's no namespace, class or Main method - this is a "top-level statement"
Console.WriteLine("Hello, World from C# 9.0!");

//-----------------------------------
// Record types
var p = new Point(100, 20);

Console.WriteLine($"({p.X}, {p.Y})");
//p.X = 400;  // <-- Error, can't set X, record properties are { get; init; } by default

var mp = new MutablePoint
{
    X = 100,
    Y = 20
};
mp.X = 400;
Console.WriteLine($"({mp.X}, {mp.Y})");


// we can omit X since it's marked `default!`
var mp2 = new MutablePoint
{
    Y = 20
};
Console.WriteLine($"({mp2.X}, {mp2.Y})");


var anon = new User
{
    Age = 100
};
Console.WriteLine($"{anon.Name.ToUpper()} - {anon.Age}");

// stringified records look good, this prints "User { Name = mr no-name, Age = 100 }"
Console.WriteLine(anon);

var bob = new User
{
    Name = "mr no-name",
    Age = 100
};

// even though they're different objects, equality is performed at property-level
Console.WriteLine(anon == bob);

Point x = new(50, 35);
Console.WriteLine(x);

var o = GetOrigin();
Console.WriteLine($"origin: {o}");

var namedAnon = anon with
{
    Name = "Jeebus"
};

Console.WriteLine(namedAnon);

var xxx = new Bar { Name = "bar" };
Console.WriteLine(GetFooName(xxx));

// borrowed the example source generator from https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/
SyntaxTreeOutputLib.SyntaxTreeOutput.Execute();
// ------------------------------------

static string GetFooName(IFoo foo)
{
    return foo.Name;
}


static Point GetOrigin()
{
    // Target-typed new - we don't need to return `new Point(0, 0)`, the type is known and can be omitted
    return new(0, 0);
}

record Point(int X, int Y);

record MutablePoint
{
    public int X { get; set; }
    public int Y { get; set; }
}

record User
{
    public string Name { get; set; } = "mr no-name";

    // NRT: similar to the behaviour of nullable reference types in C# 8.0, so I have set
    //     - <Nullable>enable</Nullable>
    //     - <TreatWarningsAsErrors>True</TreatWarningsAsErrors>

    // if Name was declared like this it would fail to build
    // public string Name { get; set; }

    // if Name was declared like the below it would fail to build
    // public string Name { get; set; } = default;

    // if Name was declared like this, the project would build but fail at runtime if no `Name` prop was supplied
    // public string Name { get; set; } = default!;
    public int Age { get; set; }
}



public interface IFoo
{
    string Name { get; }
}


// huh, records can implement interfaces
public record Bar : IFoo
{
    public string Name { get; init; } = "whatever";
}