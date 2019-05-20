using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tcp_Echo_Server {
    class Program {
        public static string data = null;
        
        static void Main(string[] args)
        {
            byte[] bytes = new Byte[1024];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Unspecified);

            Console.WriteLine("Podaj numer portu na którym serwer ma nasłuchiwać połączeń: ");
            Socket client = null;
            int port = 7;
            bool incorrectPort = true;

            do
            {
                incorrectPort = false;
                try
                {
                    port = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Podana wartość nie jest numerem portu!");
                    incorrectPort = true;
                }
            } while (incorrectPort);


                socket.Bind(new IPEndPoint(IPAddress.Parse("0.0.0.0"), port));
                
                socket.Listen(5);
                data = null;

                int counter = 0;


                while (true)
                {
                try
                {
                    client = socket.Accept();
                    
                    counter++;

                    Console.WriteLine("Połączenie z {0}", client.RemoteEndPoint.ToString());
                    Console.WriteLine("Klient numer " + counter);

                    while (SocketConnected(client))
                    {
                        try
                        {
                            byte[] bufor = new Byte[1024];
                            int size = client.Receive(bufor);

                            Console.WriteLine(Encoding.UTF8.GetString(bufor));

                            client.Send(bufor, size, SocketFlags.None);
                        }catch(SocketException ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Klient się rozłączył" );
                }
                finally
                {
                    client.Close();
                }
            }
        }

        public static bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    public static bool SocketConnected(Socket s)
    {
        bool part1 = s.Poll(1000, SelectMode.SelectRead);
        bool part2 = (s.Available == 0);
        if (part1 && part2)
            return false;
        else
            return true;
    }

    }
}
