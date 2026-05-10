using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using T.Inventory;
using UnityEngine;

public class MySQLStorageManager : MonoBehaviour, StorageInterface
{

    private string connectionString;
    private bool isConfigured = false;


    public void ConfigureDatabase(string server, string database, string username, string password, string port = "3306")
    {
        connectionString = $"Server={server};Database={database};Uid={username};Pwd={password};Port={port};Charset=utf8;Allow User Variables=true;";
        isConfigured = true;
    }

public void Initialize()
    {
        CreateTables();
        InitializeDefaultData();
    }

    private void CreateTables()
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        string createInventoryTable = @"
            CREATE TABLE IF NOT EXISTS Inventory (
                SlotIndex INT PRIMARY KEY,
                ItemID INT,
                ItemAmount INT DEFAULT 0
            );";


        string createMoneyTable = @"
            CREATE TABLE IF NOT EXISTS Money (
                ID INT PRIMARY KEY DEFAULT 1,
                Amount INT DEFAULT 1000
            );";

        ExecuteSQL(connection, createInventoryTable);
        ExecuteSQL(connection, createMoneyTable);
    }

    private void ExecuteSQL(MySqlConnection connection, string sql)
    {
        using var command = new MySqlCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    private void InitializeDefaultData()
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        string checkInventory = "SELECT COUNT(*) FROM Inventory";
        using var checkInventoryCmd = new MySqlCommand(checkInventory, connection);
        long count = Convert.ToInt64(checkInventoryCmd.ExecuteScalar());

        if (count == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                string insertSQL = "INSERT INTO Inventory (SlotIndex, ItemID, ItemAmount) VALUES (@index, 0, 0)";
                using var insertCmd = new MySqlCommand(insertSQL, connection);
                insertCmd.Parameters.AddWithValue("@index", i);
                insertCmd.ExecuteNonQuery();
            }
        }

        string checkMoney = "SELECT COUNT(*) FROM Money";
        using var checkMoneyCmd = new MySqlCommand(checkMoney, connection);
        count = Convert.ToInt64(checkMoneyCmd.ExecuteScalar());

        if (count == 0)
        {
            string insertSQL = "INSERT INTO Money (ID, Amount) VALUES (1, 1000)";
            using var insertCmd = new MySqlCommand(insertSQL, connection);
            insertCmd.ExecuteNonQuery();
        }
    }

    // 保存库存数据
    public void SaveInventoryData(List<InventoryItem> inventoryData)
    {

        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string clearSQL = "DELETE FROM Inventory";
            ExecuteSQL(connection, clearSQL);

            for (int i = 0; i < inventoryData.Count; i++)
            {
                var item = inventoryData[i];
                string insertSQL = @"
                    INSERT INTO Inventory (SlotIndex, ItemID, ItemAmount) 
                    VALUES (@SlotIndex, @ItemID, @ItemAmount)";

                using var command = new MySqlCommand(insertSQL, connection);
                command.Parameters.AddWithValue("@SlotIndex", i);
                command.Parameters.AddWithValue("@ItemID", item.ItemID);
                command.Parameters.AddWithValue("@ItemAmount", item.ItemAmount);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"保存库存数据错误: {e.Message}");
        }
    }

    // 加载库存数据
    public List<InventoryItem> LoadInventoryData()
    {
        var inventoryData = new List<InventoryItem>();

        if (!isConfigured)
        {

            for (int i = 0; i < 16; i++)
            {
                inventoryData.Add(new InventoryItem { ItemID = 0, ItemAmount = 0 });
            }
            return inventoryData;
        }

        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM Inventory ORDER BY SlotIndex";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                inventoryData.Add(new InventoryItem
                {
                    ItemID = Convert.ToInt32(reader["ItemID"]),
                    ItemAmount = Convert.ToInt32(reader["ItemAmount"])
                });
            }

        }
        catch (Exception e)
        {
            Debug.LogError($"加载库存数据错误: {e.Message}");

            for (int i = 0; i < 16; i++)
            {
                inventoryData.Add(new InventoryItem { ItemID = 0, ItemAmount = 0 });
            }
        }

        return inventoryData;
    }

    // 保存金钱数据
    public void SaveMoneyData(int moneyAmount)
    {
        if (!isConfigured)
        {
            Debug.LogError("数据库未配置");
            return;
        }

        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string sql = @"
                INSERT INTO Money (ID, Amount) 
                VALUES (1, @Amount)
                ON DUPLICATE KEY UPDATE Amount = @Amount";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Amount", moneyAmount);
            command.ExecuteNonQuery();


        }
        catch (Exception e)
        {
            Debug.LogError($"保存金钱数据错误: {e.Message}");
        }
    }

    // 加载金钱数据
    public int LoadMoneyData()
    {
        if (!isConfigured)
        {
            Debug.LogError("数据库未配置");
            return 1000;
        }

        try
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = "SELECT Amount FROM Money WHERE ID = 1";
            using var command = new MySqlCommand(query, connection);

            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                int money = Convert.ToInt32(result);
                return money;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"加载金钱数据错误: {e.Message}");
        }

        return 1000;
    }

    private void OnApplicationQuit()
    {
        SaveCurrentGameData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveCurrentGameData();
        }
    }

    public void SaveCurrentGameData()
    {
        try
        {
            var inventoryManager = InventoryManager.Instance;
            if (inventoryManager != null)
            {

                if (inventoryManager.InventoryBag_SO != null)
                {
                    SaveInventoryData(inventoryManager.InventoryBag_SO.itemList);
                }


                if (inventoryManager.MoneyDataList_SO != null &&
                    inventoryManager.MoneyDataList_SO.MoneyList != null &&
                    inventoryManager.MoneyDataList_SO.MoneyList.Count > 0)
                {
                    SaveMoneyData(inventoryManager.MoneyDataList_SO.MoneyList[0].money);
                }
                else
                {
                    SaveMoneyData(1000);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"保存游戏数据错误: {e.Message}");
        }
    }
}