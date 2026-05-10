using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace T.Inventory
{
    public class SocketManager : MonoBehaviour
    {
        private static SocketManager _instance;
        public static SocketManager Instance => _instance;

        private TcpClient _tcpClient;
        private NetworkStream _stream;
        private bool _isConnected = false;

        public bool IsConnected => _isConnected;

        private const string SERVER_IP = "127.0.0.1";
        private const int SERVER_PORT = 8888;

        public delegate void MessageCallback(string message);
        private MessageCallback _currentCallback;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void ConnectToServer()
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(SERVER_IP, SERVER_PORT);
                _stream = _tcpClient.GetStream();
                _isConnected = true;


                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessages));
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Debug.LogError($"连接服务器失败: {ex.Message}");
                _isConnected = false;
            }
        }

        public void SendMessage(string command, string data, MessageCallback callback = null)
        {
            if (!_isConnected || _stream == null)
            {
                Debug.LogError("未连接到服务器");
                return;
            }

            try
            {
                var message = new
                {
                    Command = command,
                    Data = data,
                    RequestId = Guid.NewGuid().ToString()
                };

                string jsonMessage = JsonConvert.SerializeObject(message);
                byte[] messageBytes = Encoding.UTF8.GetBytes(jsonMessage);

                // 添加消息分隔符
                byte[] endMarker = Encoding.UTF8.GetBytes("\n");
                byte[] fullMessage = new byte[messageBytes.Length + endMarker.Length];
                Array.Copy(messageBytes, 0, fullMessage, 0, messageBytes.Length);
                Array.Copy(endMarker, 0, fullMessage, messageBytes.Length, endMarker.Length);

                _stream.Write(fullMessage, 0, fullMessage.Length);

                _currentCallback = callback;

          
            }
            catch (Exception ex)
            {
                Debug.LogError($"发送消息失败: {ex.Message}");
            }
        }

        private void ReceiveMessages()
        {
            byte[] buffer = new byte[4096];
            int bytesRead;
            StringBuilder messageBuilder = new StringBuilder();

            while (_isConnected && _stream != null)
            {
                try
                {
                    bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string receivedChunk = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuilder.Append(receivedChunk);

                    string receivedData = messageBuilder.ToString();

                    // 按换行符分割消息
                    string[] messages = receivedData.Split('\n');

           
                    for (int i = 0; i < messages.Length - 1; i++)
                    {
                        if (!string.IsNullOrWhiteSpace(messages[i]))
                        {
                            string completeMessage = messages[i].Trim();
                            Debug.Log($"收到完整消息: {completeMessage}");

                            if (MainThreadDispatcher.Instance != null)
                            {
                                MainThreadDispatcher.Instance.Enqueue(() => ProcessReceivedMessage(completeMessage));
                            }
                        }
                    }

                  
                    messageBuilder = new StringBuilder(messages.Length > 0 ? messages[messages.Length - 1] : "");
                }
                catch (Exception ex)
                {
                    if (_isConnected)
                    {
                        Debug.LogError($"接收消息错误: {ex.Message}");
                    }
                    break;
                }
            }
        }

        private void ProcessReceivedMessage(string message)
        {
            try
            {

                var response = JsonConvert.DeserializeObject<SocketResponse>(message);

                if (response == null || string.IsNullOrEmpty(response.Command))
                {
                    return;
                }


                switch (response.Command)
                {
                    case "ITEMS_DATA":
                        var items = JsonConvert.DeserializeObject<List<ServerItemDetails>>(response.Data);
                        if (items != null && InventoryManager.Instance != null)
                        {
                            InventoryManager.Instance.OnItemsReceived(items);
                        }
                        break;

                    case "INVENTORY_DATA":
                        var inventory = JsonConvert.DeserializeObject<List<ServerInventoryItem>>(response.Data);
                        if (inventory != null && InventoryManager.Instance != null)
                        {
                            InventoryManager.Instance.OnInventoryReceived(inventory);
                        }
                        break;

                    case "STORE_DATA":
                        var storeItems = JsonConvert.DeserializeObject<List<ServerStoreItem>>(response.Data);
                        if (storeItems != null && InventoryManager.Instance != null)
                        {
                            InventoryManager.Instance.OnStoreItemsReceived(storeItems);
                        }
                        break;

                    case "MONEY_DATA":
                        var moneyData = JsonConvert.DeserializeObject<ServerPlayerMoney>(response.Data);
                        if (moneyData != null && InventoryManager.Instance != null)
                        {
                            InventoryManager.Instance.OnMoneyReceived(moneyData);
                        }
                        break;

                    case "UPDATE_SUCCESS":
                        Debug.Log("更新成功");
                        break;

                    case "ERROR":
                        Debug.LogError($"服务器返回错误: {response.Data}");
                        break;
                }

                _currentCallback?.Invoke(message);
                _currentCallback = null;
            }
            catch (Exception ex)
            {
                Debug.LogError($"处理消息失败: {ex.Message}\n原始消息: {message}");
            }
        }

        private void OnDestroy()
        {
            _isConnected = false;
            _stream?.Close();
            _tcpClient?.Close();
        }
    }

    [System.Serializable]
    public class SocketResponse
    {
        public string Command;
        public string Data;
    }
}