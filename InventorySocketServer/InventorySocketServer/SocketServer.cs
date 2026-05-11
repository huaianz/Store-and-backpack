using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using InventorySocketServer.Models;

namespace InventorySocketServer
{
    public class SocketServer
    {
        private TcpListener _tcpListener;
        private Thread _listenerThread;
        private bool _isRunning;
        private DatabaseContext _dbContext;
        private readonly int _port;

        public SocketServer(int port = 8888)
        {
            _port = port;
            _dbContext = new DatabaseContext();
        }

        public void Start()
        {
            _isRunning = true;
            _listenerThread = new Thread(ListenForClients);
            _listenerThread.Start();
        }

        public void Stop()
        {
            _isRunning = false;
            _tcpListener?.Stop();
        }

        private void ListenForClients()
        {
            try
            {
                _tcpListener = new TcpListener(IPAddress.Any, _port);
                _tcpListener.Start();

                while (_isRunning)
                {
                    var client = _tcpListener.AcceptTcpClient();

                    var clientThread = new Thread(HandleClient);
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"服务器错误: {ex.Message}");
            }
        }

        private void HandleClient(object clientObj)
        {
            var client = (TcpClient)clientObj;
            var stream = client.GetStream();

            try
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                StringBuilder messageBuilder = new StringBuilder();

                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    string receivedChunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuilder.Append(receivedChunk);

                    string receivedData = messageBuilder.ToString();
                    

                    string[] messages = receivedData.Split('\n');
                    
                    for (int i = 0; i < messages.Length - 1; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(messages[i]))
                        {
                            string completeMessage = messages[i].Trim();

                            ProcessMessage(completeMessage, stream);
                        }
                    }
 
                    messageBuilder = new StringBuilder(messages.Length > 0 ? messages[messages.Length - 1] : "");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"客户端处理错误: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("客户端断开连接");
            }
        }

        private void ProcessMessage(string message, NetworkStream stream)
        {
            try
            {
                var socketMessage = JsonConvert.DeserializeObject<SocketMessage>(message);
                if (socketMessage == null) return;


                string responseData = "";

                switch (socketMessage.Command)
                {
                    case "GET_ITEMS":
                        var items = _dbContext.GetAllItems();
                        responseData = JsonConvert.SerializeObject(new
                        {
                            Command = "ITEMS_DATA",
                            Data = JsonConvert.SerializeObject(items)
                        });
                        break;

                    case "GET_INVENTORY":
                        var inventory = _dbContext.GetPlayerInventory(1);
                        responseData = JsonConvert.SerializeObject(new
                        {
                            Command = "INVENTORY_DATA",
                            Data = JsonConvert.SerializeObject(inventory)
                        });
                        break;

                    case "GET_STORE_ITEMS":
                        var storeItems = _dbContext.GetStoreItems();
                        responseData = JsonConvert.SerializeObject(new
                        {
                            Command = "STORE_DATA",
                            Data = JsonConvert.SerializeObject(storeItems)
                        });
                        break;

                    case "GET_MONEY":
                        var money = _dbContext.GetPlayerMoney(1);
                        responseData = JsonConvert.SerializeObject(new
                        {
                            Command = "MONEY_DATA",
                            Data = JsonConvert.SerializeObject(money)
                        });
                        break;

                    case "UPDATE_INVENTORY":
                        var updateData = JsonConvert.DeserializeObject<dynamic>(socketMessage.Data);
                        _dbContext.UpdateInventorySlot(1, (int)updateData.slotIndex, (int)updateData.itemId, (int)updateData.amount);
                        responseData = JsonConvert.SerializeObject(new { Command = "UPDATE_SUCCESS" });
                        break;

                    case "UPDATE_MONEY":
                        var moneyData = JsonConvert.DeserializeObject<dynamic>(socketMessage.Data);
                        _dbContext.UpdatePlayerMoney(1, (int)moneyData.amount);
                        responseData = JsonConvert.SerializeObject(new { Command = "UPDATE_SUCCESS" });
                        break;

                    default:
                        responseData = JsonConvert.SerializeObject(new { Command = "ERROR", Data = "未知命令" });
                        break;
                }

                byte[] responseBytes = Encoding.UTF8.GetBytes(responseData);
                
                // 添加消息分隔符
                byte[] endMarker = Encoding.UTF8.GetBytes("\n");
                byte[] fullResponse = new byte[responseBytes.Length + endMarker.Length];
                Array.Copy(responseBytes, 0, fullResponse, 0, responseBytes.Length);
                Array.Copy(endMarker, 0, fullResponse, responseBytes.Length, endMarker.Length);

                stream.Write(fullResponse, 0, fullResponse.Length);
            }
            catch (Exception ex)
            {
                string errorResponse = JsonConvert.SerializeObject(new
                {
                    Command = "ERROR",
                    Data = JsonConvert.SerializeObject(ex.Message)
                });
                
                byte[] errorBytes = Encoding.UTF8.GetBytes(errorResponse);
                byte[] endMarker = Encoding.UTF8.GetBytes("\n");
                byte[] fullErrorResponse = new byte[errorBytes.Length + endMarker.Length];
                Array.Copy(errorBytes, 0, fullErrorResponse, 0, errorBytes.Length);
                Array.Copy(endMarker, 0, fullErrorResponse, errorBytes.Length, endMarker.Length);
                
                stream.Write(fullErrorResponse, 0, fullErrorResponse.Length);
            }
        }
    }
}