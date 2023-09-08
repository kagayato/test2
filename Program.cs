using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class TCPClient
{
    static void Main()
    {
        IPAddress serverIP = IPAddress.Parse("127.0.0.1");
        int serverPort = 12345;

        TcpClient client = new TcpClient();
        client.Connect(serverIP, serverPort);
        Console.WriteLine("Подключено к серверу.");

        NetworkStream stream = client.GetStream();
        StreamReader reader = new StreamReader(stream);
        StreamWriter writer = new StreamWriter(stream);

        try
        {
            while (true)
            {
                Console.WriteLine("Введите команду (1 - камера, 2 - толкатель, 0 - выход): ");
                string input = Console.ReadLine();

                if (input == "0")
                {
                    break;
                }

                writer.WriteLine(input);
                writer.Flush();

                string response = reader.ReadLine();
                Console.WriteLine("Сервер: " + response);

                // Если сервер запрашивает выбор "годный" или "брак", то получите выбор от пользователя
                if (response == "Введите 'годный' или 'брак': ")
                {
                    string choice = Console.ReadLine();
                    writer.WriteLine(choice);
                    writer.Flush();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        finally
        {
            client.Close();
        }
    }
}
