using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDetails
{
    public int ItemID;
    public string ItemName;
    public int ItemPrice;
    public Sprite ItemSprite;
    public string ItemSpriteName;
}

[System.Serializable]
public class InventoryItem
{
    public int SlotIndex;
    public int ItemID;
    public int ItemAmount;
}

[System.Serializable]
public class Store
{
    public int ItemID;
    public int ItemPrice;
    public int StorePrice;
}

[System.Serializable]
public class Money
{
    public int money;
}

[System.Serializable]
public class SceneItem
{
    public int itemID;
    public Vector3 position;
}

[System.Serializable]
public class MySqlData
{
    public string Server = "localhost";
    public string Database = "game_inventory_db";
    public string Username = "gameser";
    public string Password = "gamepassword";
    public string Port = "3306";
}



[System.Serializable]
public class SocketMessage
{
    public string Command;
    public string Data;
    public string RequestId;
}

[System.Serializable]
public class ServerItemDetails
{
    public int ItemID;
    public string ItemName;
    public int ItemPrice;
    public string ItemSpriteName;
}

[System.Serializable]
public class ServerInventoryItem
{
    public int SlotIndex;
    public int ItemID;
    public int ItemAmount;
}

[System.Serializable]
public class ServerStoreItem
{
    public int ItemID;
    public int StorePrice;
}

[System.Serializable]
public class ServerPlayerMoney
{
    public int MoneyAmount;
}

[System.Serializable]
public class SocketResponse
{
    public string Command;
    public string Data;
}