using System;
using Fundamentals.Standard;

namespace Fundamentals.Core
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException($"Expected one command line parameter, but was {args.Length}.");
            }

            var username = args[0];
            Console.WriteLine(Concatenator.GetGreetingMessage(username));
        }
    }
}