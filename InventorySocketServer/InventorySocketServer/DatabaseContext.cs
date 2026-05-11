using InventorySocketServer.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace InventorySocketServer
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext()
        {
            _connectionString = "Server=localhost;Database=game_inventory_db;Uid=gameser;Pwd=gamepassword;";
        }

        public List<ItemDetails> GetAllItems()
        {
            var items = new List<ItemDetails>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT item_id, item_name, item_price, item_sprite_name FROM items", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new ItemDetails
                        {
                            ItemID = reader.GetInt32("item_id"),
                            ItemName = reader.GetString("item_name"),
                            ItemPrice = reader.GetInt32("item_price"),
                            ItemSpriteName = reader.GetString("item_sprite_name")
                        });
                    }
                }
            }

            return items;
        }

        public List<InventoryItem> GetPlayerInventory(int playerId = 1)
        {
            var inventory = new List<InventoryItem>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "SELECT slot_index, item_id, item_amount FROM player_inventory WHERE player_id = @playerId ORDER BY slot_index",
                    connection);
                command.Parameters.AddWithValue("@playerId", playerId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inventory.Add(new InventoryItem
                        {
                            SlotIndex = reader.GetInt32("slot_index"),
                            ItemID = reader.IsDBNull("item_id") ? 0 : reader.GetInt32("item_id"),
                            ItemAmount = reader.GetInt32("item_amount")
                        });
                    }
                }
            }

            return inventory;
        }

        public List<StoreItem> GetStoreItems()
        {
            var storeItems = new List<StoreItem>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "SELECT si.item_id, si.store_price FROM store_items si",
                    connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        storeItems.Add(new StoreItem
                        {
                            ItemID = reader.GetInt32("item_id"),
                            StorePrice = reader.GetInt32("store_price")
                        });
                    }
                }
            }

            return storeItems;
        }

        public PlayerMoney GetPlayerMoney(int playerId = 1)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "SELECT money_amount FROM player_money WHERE player_id = @playerId",
                    connection);
                command.Parameters.AddWithValue("@playerId", playerId);

                var result = command.ExecuteScalar();
                return new PlayerMoney { MoneyAmount = result != null ? System.Convert.ToInt32(result) : 0 };
            }
        }

        public void UpdateInventorySlot(int playerId, int slotIndex, int itemId, int amount)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                if (amount > 0 && itemId > 0)
                {
                    var command = new MySqlCommand(
                        @"INSERT INTO player_inventory (player_id, slot_index, item_id, item_amount) 
                          VALUES (@playerId, @slotIndex, @itemId, @amount)
                          ON DUPLICATE KEY UPDATE item_id = @itemId, item_amount = @amount",
                        connection);
                    command.Parameters.AddWithValue("@playerId", playerId);
                    command.Parameters.AddWithValue("@slotIndex", slotIndex);
                    command.Parameters.AddWithValue("@itemId", itemId);
                    command.Parameters.AddWithValue("@amount", amount);
                    command.ExecuteNonQuery();
                }
                else
                {
                    var command = new MySqlCommand(
                        "UPDATE player_inventory SET item_id = 0, item_amount = 0 WHERE player_id = @playerId AND slot_index = @slotIndex",
                        connection);
                    command.Parameters.AddWithValue("@playerId", playerId);
                    command.Parameters.AddWithValue("@slotIndex", slotIndex);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePlayerMoney(int playerId, int amount)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand(
                    "UPDATE player_money SET money_amount = @amount WHERE player_id = @playerId",
                    connection);
                command.Parameters.AddWithValue("@playerId", playerId);
                command.Parameters.AddWithValue("@amount", amount);
                command.ExecuteNonQuery();
            }
        }
    }
}