using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface StorageInterface
{
    public void Initialize();
    public void SaveInventoryData(List<InventoryItem> inventoryData);
    public List<InventoryItem> LoadInventoryData();
    public void SaveMoneyData(int moneyAmount);
    public int LoadMoneyData();
    public void SaveCurrentGameData();
}
