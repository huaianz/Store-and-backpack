using UnityEngine;
using System.Collections;
using T.Inventory;

public class InventoryInitializers : MonoBehaviour
{
    [Header("初始化设置")]
    public bool initializeStart = true;
    public int BagSize = 16;
    public int Money = 1000;

    private void Start()
    {
        if (initializeStart)
        {
            StartCoroutine(DelayedInitialize());
        }
    }

    private IEnumerator DelayedInitialize()
    {
        yield return null;
        InitializeInventoryData();
    }

    private void InitializeInventoryData()
    {
        try
        {
            var inventoryManager = InventoryManager.Instance;

            // 确保背包列表初始化
            if (inventoryManager.InventoryBag_SO.itemList == null)
            {
                inventoryManager.InventoryBag_SO.itemList = new System.Collections.Generic.List<InventoryItem>();
            }


            if (inventoryManager.InventoryBag_SO.itemList.Count == 0)
            {
                for (int i = 0; i < BagSize; i++)
                {
                    inventoryManager.InventoryBag_SO.itemList.Add(new InventoryItem
                    {
                        ItemID = 0,
                        ItemAmount = 0
                    });
                }
            }


            if (inventoryManager.MoneyDataList_SO.MoneyList == null)
            {
                inventoryManager.MoneyDataList_SO.MoneyList = new System.Collections.Generic.List<Money>();
            }

            if (inventoryManager.MoneyDataList_SO.MoneyList.Count == 0)
            {
                inventoryManager.MoneyDataList_SO.MoneyList.Add(new Money
                {
                    money = Money
                });
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, inventoryManager.InventoryBag_SO.itemList);

        }
        catch (System.Exception e)
        {
            Debug.LogError($"初始化库存数据时出错: {e.Message}");
        }
    }

    [ContextMenu("手动重新初始化")]
    public void ManualReinitialize()
    {
        InitializeInventoryData();
    }
}