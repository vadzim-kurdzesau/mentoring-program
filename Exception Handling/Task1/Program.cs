using System;

namespace Task1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    Console.WriteLine(input[0]);
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Do not enter the empty lines");
                }
            }
        }
    }
}