using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using T.Inventory;
using UnityEngine;

public class SQLiteStorageManager : MonoBehaviour, StorageInterface
{

    private string dbPath;
    private string connectionString;


    //初始化
    public void Initialize()
    {
        try
        {
            dbPath = Path.Combine(Application.persistentDataPath, "GameData.db");

            //connectionString = $"Data Source={dbPath};Version=3;";
            connectionString = "Data Source=" + dbPath + ";Version=3;";
            CreateTables();
        }
        catch (Exception e)
        {
            Debug.LogError($"数据库初始化失败: {e.Message}");
        }
    }

    private void CreateTables()
    {
        using var connection = new SQLiteConnection(connectionString);
        connection.Open();

        // 创建背包表
        string createInventoryTable = @"
            CREATE TABLE IF NOT EXISTS Inventory (
                SlotIndex INTEGER PRIMARY KEY,
                ItemID INTEGER,
                ItemAmount INTEGER DEFAULT 0
            );";

        // 创建金钱表
        string createMoneyTable = @"
            CREATE TABLE IF NOT EXISTS Money (
                ID INTEGER PRIMARY KEY CHECK (ID = 1),
                Amount INTEGER DEFAULT 1000
            );";

        //分别执行两个表的sql语句
        ExecuteSQL(connection, createInventoryTable);
        ExecuteSQL(connection, createMoneyTable);

        // 初始化数据
        InitializeData(connection);
    }

    private void ExecuteSQL(SQLiteConnection connection, string sql)
    {
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    private void InitializeData(SQLiteConnection connection)
    {
        // 检查背包数据
        string checkInventory = "SELECT COUNT(*) FROM Inventory";
        using var checkInventoryCmd = new SQLiteCommand(checkInventory, connection);
        long count = (long)checkInventoryCmd.ExecuteScalar();

        if (count == 0)
        {
            for (int i = 0; i < 16; i++)
            {
                string insertSQL = "INSERT INTO Inventory (SlotIndex, ItemID, ItemAmount) VALUES (@index, 0, 0)";
                using var insertCmd = new SQLiteCommand(insertSQL, connection);
                insertCmd.Parameters.AddWithValue("@index", i);
                insertCmd.ExecuteNonQuery();
            }
        }

        // 检查金钱数据
        string checkMoney = "SELECT COUNT(*) FROM Money";
        using var checkMoneyCmd = new SQLiteCommand(checkMoney, connection);
        count = (long)checkMoneyCmd.ExecuteScalar();

        if (count == 0)
        {
            string insertSQL = "INSERT INTO Money (ID, Amount) VALUES (1, 1000)";
            using var insertCmd = new SQLiteCommand(insertSQL, connection);
            insertCmd.ExecuteNonQuery();
        }
    }

    // 保存背包数据
    public void SaveInventoryData(List<InventoryItem> inventoryData)
    {
        try
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();


            string clearSQL = "DELETE FROM Inventory";
            ExecuteSQL(connection, clearSQL);

            for (int i = 0; i < inventoryData.Count; i++)
            {
                var item = inventoryData[i];
                string insertSQL = @"
                    INSERT INTO Inventory (SlotIndex, ItemID, ItemAmount) 
                    VALUES (@SlotIndex, @ItemID, @ItemAmount)";

                using var command = new SQLiteCommand(insertSQL, connection);
                command.Parameters.AddWithValue("@SlotIndex", i);
                command.Parameters.AddWithValue("@ItemID", item.ItemID);
                command.Parameters.AddWithValue("@ItemAmount", item.ItemAmount);
                command.ExecuteNonQuery();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"保存背包数据失败: {e.Message}");
        }
    }

    // 加载背包数据
    public List<InventoryItem> LoadInventoryData()
    {
        var inventoryData = new List<InventoryItem>();

        try
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            string query = "SELECT * FROM Inventory ORDER BY SlotIndex";
            //ORDER BY SlotIndex 对查询到的数据按SlotIndex排序
            using var command = new SQLiteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                inventoryData.Add(new InventoryItem
                {
                    ItemID = Convert.ToInt32(reader["ItemID"]),
                    ItemAmount = Convert.ToInt32(reader["ItemAmount"])
                });

                //var item = new InventoryItem()
                //{
                //    ItemID = Convert.ToInt32(reader["ItemID"]),
                //    ItemAmount = Convert.ToInt32(reader["ItemAmount"])
                //};
                //inventoryData.Add(item);

            }

        }
        catch (Exception e)
        {
            Debug.LogError($"加载背包数据失败: {e.Message}");
            // 返回空背包
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
        try
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            string sql = @"
                INSERT OR REPLACE INTO Money (ID, Amount) 
                VALUES (1, @Amount)";

            using var command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@Amount", moneyAmount);
            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            Debug.LogError($"保存金钱数据失败: {e.Message}");
        }
    }

    // 加载金钱数据
    public int LoadMoneyData()
    {
        try
        {
            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            string query = "SELECT Amount FROM Money WHERE ID = 1";
            using var command = new SQLiteCommand(query, connection);

            var result = command.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                int money = Convert.ToInt32(result);
                return money;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"加载金钱数据失败: {e.Message}");
        }

        return 1000;
    }


    public void SaveCurrentGameData()
    {
        try
        {
            var inventoryManager = InventoryManager.Instance;
            if (inventoryManager != null)
            {
                // 保存背包数据
                var bagSO = inventoryManager.InventoryBag_SO;
                if (bagSO != null)
                {
                    SaveInventoryData(bagSO.itemList);
                }

                // 保存金钱数据
                var moneySO = inventoryManager.MoneyDataList_SO;
                if (moneySO != null && moneySO.MoneyList != null && moneySO.MoneyList.Count > 0)
                {
                    SaveMoneyData(moneySO.MoneyList[0].money);
                }
                else
                {
                    SaveMoneyData(1000); // 默认值
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"保存游戏数据失败: {e.Message}");
        }
    }
}