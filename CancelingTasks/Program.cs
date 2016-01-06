using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancelingTasks
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //CancellationTokenTest();
            //OperationCanceledException();
            ContinuationForCanceledTasks();
        }

        private static void CancellationTokenTest()
        {
            var cancellationTokenSource =
                new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var task = Task.Run(() =>
            {
                while (true)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
            }, token);
            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }

        private static void OperationCanceledException()
        {
            var cancellationTokenSource =
                new CancellationTokenSource();
            CancellationTokenTest();
            var token = cancellationTokenSource.Token;
            var task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                token.ThrowIfCancellationRequested();
            });
            try
            {
                Console.WriteLine("Press enter to stop the task");
                Console.ReadLine();
                cancellationTokenSource.Cancel();
                task.Wait(token);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.InnerExceptions[0].Message);
            }
            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }

        private static void ContinuationForCanceledTasks()
        {
            var cancellationTokenSource =
                new CancellationTokenSource();
            var token = cancellationTokenSource.Token;
            var task = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Console.Write("*");
                    Thread.Sleep(1000);
                }
                throw new OperationCanceledException();
            }, token).ContinueWith(t =>
            {
                t.Exception.Handle(e => true);
                Console.WriteLine("You have canceled the task");
            }, TaskContinuationOptions.OnlyOnCanceled);

            Console.WriteLine("Press enter to stop the task");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
            Console.WriteLine("Press enter to end the application");
            Console.ReadLine();
        }
    }
}