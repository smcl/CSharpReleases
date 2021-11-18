using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World from C# 8.0!");

            var anon = new User
            {
                Age = 100
            };
            Console.WriteLine($"{anon.Name.ToUpper()} - {anon.Age}");

            var bob = new User
            {
                Name = "Bob",
                Age = 50
            };

            Console.WriteLine($"Admin can edit: {CanEdit(Role.Admin)}");

            var sean = new User
            {
                Name = "Sean",
                Age = 35
            };
            Console.WriteLine($"anon ({anon.Age}) - {JudgeAge(anon)}");
            Console.WriteLine($"bob  ({bob.Age}) - {JudgeAge(bob)}");
            Console.WriteLine($"sean ({sean.Age}) - {JudgeAge(sean)}");

            Console.WriteLine(AddOne(0, 1));

            Console.WriteLine(ClassifyGeneration(anon.Age));
            Console.WriteLine(ClassifyGeneration(bob.Age));
            Console.WriteLine(ClassifyGeneration(sean.Age));

            Console.WriteLine(Foo(100));

            var dudes = ParseDudes().ToList();

            Console.WriteLine($"the last dude is {dudes[^1]}");
            Console.WriteLine($"the second-last dude is {dudes[^2]}");
        }

        static IEnumerable<string> ParseDudes()
        {
            var dudeString = @"name,age
anon,100
bob,50
sean,35";

            // skip the header row
            var dudeLines = dudeString.Split("\n")[1..];

            foreach (var dudeLine in dudeLines)
            {
                var cols = dudeLine.Split(',');
                var (name, age) = (cols[0], cols[1]);
                yield return $"{name} -> {age}";
            }
        }

        static int Foo(int a)
        {
            return Add10(a) + Add100();
            int Add100() => a + 100;
            static int Add10(int x) => x + 10;// note, we're static so we can't refer to `a`
            // static int Add10() => a + 10;  // so this for example would fail to compile

        }

        static string ClassifyGeneration(int age)
            => age switch
            {
                var a when a < 30 => "Zoomer",
                var a when a >= 30 && a < 40 => "Millenial",
                var a when a >= 40 && a < 55 => "Gen X",
                var a when a >= 55 && a < 70 => "Boomer",
                var a when a >= 70 && a < 90 => "air-quotes Greatest",
                var a when a >= 90 => "Silent",
                _ => "THE MYSTERY GENERATION"
            };

        static (int, int, int) AddOne(int a, int b)
        {
            return (a, b) switch
            {
                (0, 0) => (0, 0, 1),
                (0, 1) => (0, 1, 0),
                (1, 0) => (0, 1, 1),
                (1, 1) => (1, 0, 0),
                _ => throw new ArgumentOutOfRangeException("this is for binary only, ya big goof")
            };
        }

        static string JudgeAge(User user) =>
            user switch
            {
                { Age: 35 } => "Perfect age to be, glorious",
                _ => "Garbage age"
            };

        static bool CanEdit(Role role)
        {
            return role switch
            {
                Role.Admin => true,
                Role.Manager => true,
                Role.God => true,
                _ => false
            };
        }
    }

    class User
    {
        public string Name { get; set; } = "mr no-name";

        // NRT: I have set
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

    enum Role
    {
        User,
        Manager,
        Admin,
        God
    }
}
