using System;
using System.Threading;


namespace MultiThreating2 {
    class Program {

        static void SimpleThread(string number)
        {
            Console.WriteLine((char)65);
        }

        static void Main(string[] args)
        {
            Thread t = new Thread(() => SimpleThread("1"));
            t.Start();


            Console.Read();
        }
    }
}
