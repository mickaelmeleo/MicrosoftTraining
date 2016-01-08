using System;
using NUnit.Framework;

namespace Tester
{
    public class Various
    {
        // Usually we have to write:
        // word => Console.WriteLine(word);
        public static Action<string> Print = Console.WriteLine;

        [Test]
        public void AbbreviatedForm()
        {
            // Usually we have to write 
            // Console.WriteLine();
            Action print = Console.WriteLine;

            Console.WriteLine("Start...");

            // Act
            print();
            Print("Hello World");
        }
    }
}