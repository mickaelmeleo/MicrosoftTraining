using System;
using System.Threading;
using NUnit.Framework;

namespace CSharp.ProgramFlow
{
    public class Threads
    {
        [ThreadStatic]
        private int _foo = Thread.CurrentThread.ManagedThreadId;

        public static ThreadLocal<int> Field =
            new ThreadLocal<int>(() => { return Thread.CurrentThread.ManagedThreadId; });

        static void MethodUseless(object i)
        {
        }
        static void Start(object info)
        {
            // This receives the value passed into the Thread.Start method.
            int value = (int)info;
            Console.WriteLine(value);
        }
        [Test]
        public void CreateThread_Action()
        {
            // action
            Action threadMethod = () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine("ThreadProc: {0}", i);
                    Thread.Sleep(0);
                }
            };
            var t = new Thread(new ThreadStart(threadMethod));

            // action with parameter
            Action<int> threadMethodParam = c =>
            {
                for (var i = 0; i < c; i++)
                {
                    Console.WriteLine("ThreadProcParam: {0}", i);
                    Thread.Sleep(0);
                }
            };

            // init thread with action parametized
            var tParam = new Thread(obj => threadMethodParam(11));
            var tparam2 = new Thread(new ParameterizedThreadStart(MethodUseless));


            t.Start();
            tParam.Start();

            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }
            t.Join();
            tParam.Join();

            Console.WriteLine("Done!.");
        }

        [Test]
        public void LocalData_ThreadStatic_AndThreadLocal()
        {
            // [ThreadStatic] doesn't automatically initialize things for every thread
            // ThreadStatic Initialize only on first thread, ThreadLocal Initialize for each thread.
            Console.WriteLine("Test with ThreadLocal");

            var thread = new Thread(() =>
            {
                Console.WriteLine("First Thread: {0}", Field.Value);
            });
            thread.Start();

            var thread2 = new Thread(() =>
            {
                Console.WriteLine("Second Thread: {0}", Field.Value);
            });
            thread2.Start();

            thread.Join();
            thread.Join();

            Console.WriteLine();
            Console.WriteLine("Test with ThreadStatic");

            var thread3 = new Thread(() =>
            {
                Console.WriteLine("First Thread: {0}", _foo);
            });
            thread3.Start();

            var thread4 = new Thread(() =>
            {
                Console.WriteLine("Second Thread: {0}", _foo);
            });
            thread4.Start();

            thread3.Join();
            thread4.Join();
        }
    }
}