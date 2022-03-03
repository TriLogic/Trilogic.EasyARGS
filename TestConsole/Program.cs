using System;

using Trilogic.EasyARGS;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var settings = ArgSettings.ParseSettings(args);

            Console.WriteLine("Hello World!");
        }
    }
}
