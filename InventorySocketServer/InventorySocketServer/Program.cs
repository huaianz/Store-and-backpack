using System;

namespace InventorySocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("背包系统Socket服务器启动中...");

            var server = new SocketServer(8888);
            server.Start();

            Console.WriteLine("按任意键停止服务器...");
            Console.ReadKey();

            server.Stop();
        }
    }
}