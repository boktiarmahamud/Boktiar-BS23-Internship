using System;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpFundamentals
{
    internal class AsynchronousProgramming
    {
        public static async Task Main(string[] args)
        {
            Task t1 = Work1();
            Task t2 = Work2();
            Task t3 = Work3();

            await Task.WhenAll(t1, t2, t3);

            Console.WriteLine("All works completed.");
        }

        public static async Task Work1()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Work 1 is starting...");
                Thread.Sleep(4000);
                Console.WriteLine("Work 1 is completed.");
            });
        }

        public static async Task Work2()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Work 2 is starting...");
                Thread.Sleep(2000);
                Console.WriteLine("Work 2 is completed.");
            });
        }

        public static async Task Work3()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Work 3 is starting...");
                Thread.Sleep(3000);
                Console.WriteLine("Work 3 is completed.");
            });
        }
    }
}