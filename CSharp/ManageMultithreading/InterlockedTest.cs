using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CSharp
{
    public class InterlockedClass
    {
        private static int _value = 1;

        //[SetUp]
        //public void TestInitialize()
        //{
        //    _value = 1;
        //}

        [Test]
        public void NoLock_Test()
        {
            var n = 0;
            var up = Task.Run(() =>
            {
                for (var i = 0; i < 1000000; i++)
                    n++;
            });
            for (var i = 0; i < 1000000; i++)
                n--;
            up.Wait();
            Console.WriteLine(n);
        }

        [Test]
        public void Interlocked_IncrementDecrement_Test()
        {
            var n = 0;
            var up = Task.Run(() =>
            {
                for (var i = 0; i < 1000000; i++)
                    Interlocked.Increment(ref n);
            });

            for (var i = 0; i < 1000000; i++)
                Interlocked.Decrement(ref n);

            up.Wait();
            Console.WriteLine(n);
        }

        // -----------------------------------------------------------------------
        // Compare and exchange value
        // -----------------------------------------------------------------------

        [Test]
        public void CompareExchange_NonAtomicOperation_Test()
        {
            var t1 = Task.Run(() =>
            {
                Console.WriteLine("before " + _value); // Displays 2

                if (_value == 1)
                {
                    // Removing the following line will change the output
                    Thread.Sleep(1000);
                    _value = 2;
                }
            });
            var t2 = Task.Run(() => { _value = 3; });
            Task.WaitAll(t1, t2);
            Console.WriteLine(_value); // Displays 2
        }

        [Test]
        public void CompareExchange_AtomicOperation_Test()
        {
            var t1 = Task.Run(() => { Interlocked.CompareExchange(ref _value, 2, 1); });

            var t2 = Task.Run(() => { _value = 4; });

            Task.WaitAll(t1, t2);
            Console.WriteLine(_value); // Displays 3
        }
    }
}