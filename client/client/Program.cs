using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        int serverPort = 12345;
        TcpClient client = new TcpClient();
        client.Connect(serverIP, serverPort);
        Console.WriteLine("Виберіть 'date' або 'time':");
        string request = Console.ReadLine();
        NetworkStream stream = client.GetStream();
        byte[] data = Encoding.UTF8.GetBytes(request);
        stream.Write(data, 0, data.Length);
        byte[] responseData = new byte[256];
        int bytes = stream.Read(responseData, 0, responseData.Length);
        string response = Encoding.UTF8.GetString(responseData, 0, bytes);
        Console.WriteLine("Отримано від сервера: " + response);
        client.Close();
    }
}
