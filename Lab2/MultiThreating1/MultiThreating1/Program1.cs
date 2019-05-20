using System;
using System.Threading;

namespace MultiThreating1 {
    class Program1 {

        static void HelloWorld()
        {
            Console.WriteLine("Hello World!");
        }


        static void Main(string[] args)
        {
            Thread thread = new Thread(HelloWorld);

            
            thread.Start();
            thread.Join();
        }
    }
}
