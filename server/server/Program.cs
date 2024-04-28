using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 12345;
        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();
        Console.WriteLine("Сервер запущено...");
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Клієнт підключений.");
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[256];
            int bytes = stream.Read(data, 0, data.Length);
            string request = Encoding.UTF8.GetString(data, 0, bytes);
            Console.WriteLine("Запит від клієнта: " + request);
            string response = "";
            if (request == "date")
            {
                response = DateTime.Now.ToShortDateString();
            }
            else if (request == "time")
            {
                response = DateTime.Now.ToShortTimeString();
            }
            else
            {
                response = "Невірний запит";
            }

            byte[] responseData = Encoding.UTF8.GetBytes(response);
            stream.Write(responseData, 0, responseData.Length);
            Console.WriteLine("Відправлено: " + response);
            client.Close();
            Console.WriteLine("З'єднання розірвано.");
        }
    }
}
