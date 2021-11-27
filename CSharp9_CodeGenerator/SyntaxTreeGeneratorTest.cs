
using System;
namespace SyntaxTreeOutputLib
{
    public static class SyntaxTreeOutput 
    {
        public static void Execute()
        {
            var messages = new string[] {
                "Whattup it's ya boy",
                "C:/Users/Sean/source/repos/CSharpDemo/CSharpReleases/CSharp9/Program.cs",
                "C:/Users/Sean/source/repos/CSharpDemo/CSharpReleases/CSharp9/obj/Debug/net5.0/CSharp_9.AssemblyInfo.cs",
                "C:/Users/Sean/source/repos/CSharpDemo/CSharpReleases/CSharp9/obj/Debug/net5.0/.NETCoreApp,Version=v5.0.AssemblyAttributes.cs",

            };

            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}