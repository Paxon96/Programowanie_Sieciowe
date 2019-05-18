using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tcp_Client {
    class Program {
        static void Main(string[] args)
        {

            Console.WriteLine("Starting ECHO Client ... ");

            String ipAdress = "127.0.0.1";

            Console.WriteLine("Podaj numer portu: ");

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


            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(new IPEndPoint(IPAddress.Parse(ipAdress), port));
                Console.WriteLine("Nastąpiło połączenie");
                while (true)
                {
                    Console.WriteLine("Wpisz wiadomość: ");
                    String message = Console.ReadLine();

                    if (message == "exit")
                        break;
                    if (message.Trim() == "")
                        continue;

                    socket.Send(Encoding.ASCII.GetBytes(message));

                    byte[] buffer = new byte[1024];
                    int result = socket.Receive(buffer);
                    String response = Encoding.ASCII.GetString(buffer, 0, result);

                    Console.WriteLine("ECHO: " + response);
                }

            }
            catch (SocketException ex)
            {
                Console.WriteLine("Błąd połączena z serwerem na adresie " + ipAdress);
            }

            Console.WriteLine("Zamykam");
            Console.ReadLine();

        }
    }
}
