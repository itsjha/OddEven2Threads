using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OddEven2Threads
{
    class Program
    {
        static EventWaitHandle evenReady;
        static EventWaitHandle oddReady;

        static void Main(string[] args)
        {
            bool countOdd = true; // Change these to true/false as wanted.
            bool countEven = true;

            if (countOdd && countEven)
            {
                evenReady = new AutoResetEvent(false);
                oddReady = new AutoResetEvent(true); // Must be true for the starting thread.
            }
            else
            {
                evenReady = new ManualResetEvent(true);
                oddReady = new ManualResetEvent(true);
            }

            Thread countThreadOdd = new Thread(oddThread);
            Thread countThreadEven = new Thread(evenThread);

            //Thread Start
            if (countOdd)
                countThreadOdd.Start();

            if (countEven)
                countThreadEven.Start();

            //main thread will wait untill below thread is in execution mode

            if (countOdd)
                countThreadOdd.Join();

            if (countEven)
                countThreadEven.Join();

            Console.WriteLine("\n \n \t \t \t ****Done***");
            Console.ReadLine();
        }

        public static void oddThread()
        {
            for (int i = 1; i < 10; i += 2)
            {
                evenReady.WaitOne();
                Console.Write(i);
                Console.WriteLine("\t :  Written from thread 2 : Odd thread");
                oddReady.Set();
            }
        }

        public static void evenThread()
        {
            for (int i = 0; i < 10; i += 2)
            {
                oddReady.WaitOne();
                Console.Write(i);
                Console.WriteLine("\t :  Written from thread 1 : Even thread");
                evenReady.Set();
            }
        }
    }
}
