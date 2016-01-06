using System;
using NUnit.Framework;

namespace CSharp.EventsAndCallbacks
{
    internal class DelegatesTest
    {
        public delegate int Calculate(int x, int y);

        public int Add(int x, int y)
        {
            return x + y;
        }

        public int Multiply(int x, int y)
        {
            return x*y;
        }

        [Test]
        public void Delegates_Delegate_Test()
        {
            Calculate calc = Add;
            Console.WriteLine(calc(3, 4)); // Displays 7

            calc = Multiply;
            Console.WriteLine(calc(3, 4)); // Displays 12
        }
    }
}