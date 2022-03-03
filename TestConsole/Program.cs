using System;

using Trilogic.EasyARGS;

namespace TestConsole
{
    internal class Program
    {
        static string[] stuff = { "b", "f" };
        static string[] notstuff = { "x", "y", "z"};

        static void Main(string[] args)
        {
            var settings = ArgSettings.ParseSettings(args);

            settings.AssertOr(stuff, "somthing's missing");

            string temp = settings.ValueOf(notstuff, "missing");

            Console.WriteLine("Hello World!");
        }
    }
}
