using System;
using MovieHelper.Logic;

namespace MovieHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandRunner.RunCommands(new ArgumentsParser(args), new CoreHelper(), GetCurrentDirectory());
        }

        private static string GetCurrentDirectory()
        {
            return Environment.CurrentDirectory;
        }
    }
}
