using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace InventorySocketServer.Models
{
    [Serializable]
    public class SocketMessage
    {
        public string Command { get; set; }
        public string Data { get; set; }
        public string RequestId { get; set; }
    }

    [Serializable]
    public class ItemDetails
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int ItemPrice { get; set; }
        public string ItemSpriteName { get; set; }
    }

    [Serializable]
    public class InventoryItem
    {
        public int SlotIndex { get; set; }
        public int ItemID { get; set; }
        public int ItemAmount { get; set; }
    }

    [Serializable]
    public class StoreItem
    {
        public int ItemID { get; set; }
        public int StorePrice { get; set; }
    }

    [Serializable]
    public class PlayerMoney
    {
        public int MoneyAmount { get; set; }
    }

    [Serializable]
    public class SceneItem
    {
        public int ItemID { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float PositionZ { get; set; }
    }
}