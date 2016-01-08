using System;
using System.Dynamic;
using System.Threading;
using NUnit.Framework;

namespace ManageProgramFlow
{
    public class ThreadTraining
    {


        [Test]
        public void CreateThread_WithMethod()
        {
            var threadStart = new ThreadStart(MethodThread);
            var parameterizedThreadStart = new ParameterizedThreadStart(MethodThreadParameterized);


            var task = new Thread(MethodThreadParameterized);
            var taskParameterized = new Thread(MethodThread);

            task.Start();
            taskParameterized.Start(10);
        }

        [Test]
        public void CreateThread_Action_NoParameters()
        {
            Action threadMethod = () =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine("threadMethod: {0}", i);
                    Thread.Sleep(0);
                }
            };


            var threadStart = new ThreadStart(threadMethod);
             //new ParameterizedThreadStart(MethodThreadParameterized);
            

            var task = new Thread(threadStart);
            task.Start();

            DoSomeWork();

            task.Join();
        }

        [Test]
        [Description("Create a thread with an action as Delegate and an object as parameter.")]
        public void CreateThread_Action_Parameterized()
        {
            Console.WriteLine("Start thread...");

            Action<int> methodThreadParameterized = c =>
            {
                for (var i = 0; i < c; i++)
                {
                    Console.WriteLine("methodThreadParameterized: {0}", i);
                    Thread.Sleep(0);
                }
            };

            var taskLambda = new Thread(obj => methodThreadParameterized(10));

            // No param, already add into the lambda expression
            taskLambda.Start();
            
            DoSomeWork();

            taskLambda.Join();
        }



        [Test]
        public void LocalData_ThreadStatic_AndThreadLocal()
        {
            // [ThreadStatic] doesn't automatically initialize things for every thread
            // ThreadStatic Initialize only on first thread, ThreadLocal Initialize for each thread.
            Console.WriteLine("Test with ThreadLocal");

            //var thread = new Thread(() => { Console.WriteLine("First Thread: {0}", _threadLocalData.Value); });
            //thread.Start();

            //var thread2 = new Thread(() => { Console.WriteLine("Second Thread: {0}", _threadLocalData.Value); });
            //thread2.Start();

            //thread.Join();
            //thread.Join();

            Console.WriteLine();
            Console.WriteLine("Test with ThreadStatic");

            //var thread3 = new Thread(() => { Console.WriteLine("First Thread: {0}", _foo); });
            //thread3.Start();

            //var thread4 = new Thread(() => { Console.WriteLine("Second Thread: {0}", _foo); });
            //thread4.Start();

            //thread3.Join();
            //thread4.Join();
        }

        #region Private

        private void MethodThread()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("MethodThread: {0}", i);
                Thread.Sleep(0);
            }
        }

        private void MethodThreadParameterized(object obj)
        {
            for (var i = 0; i < (int)obj; i++)
            {
                Console.WriteLine("MethodThreadParameterized: {0}", i);
                Thread.Sleep(0);
            }
        }

        private void DoSomeWork()
        {
            for (var i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: Do some work.");
                Thread.Sleep(0);
            }
        }

        #endregion
    }
}