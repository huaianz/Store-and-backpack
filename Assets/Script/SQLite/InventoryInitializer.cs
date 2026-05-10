//using UnityEngine;
//using System.Collections;
//using T.Inventory;

//public class InventoryInitializer : MonoBehaviour
//{
//    [Header("初始化设置")]
//    public bool initializeOnStart = true;
//    public int defaultBagSize = 20;
//    public int defaultMoney = 1000;

//    private void Start()
//    {
//        if (initializeOnStart)
//        {
//            StartCoroutine(DelayedInitialize());
//        }
//    }

//    private IEnumerator DelayedInitialize()
//    {
//        yield return null;

//        InitializeInventoryData();
//    }

//    private void InitializeInventoryData()
//    {
//        try
//        {
//            var inventoryManager = InventoryManager.Instance;

//            // 确保背包列表初始化
//            if (inventoryManager.InventoryBag_SO.itemList == null)
//            {
//                inventoryManager.InventoryBag_SO.itemList = new System.Collections.Generic.List<InventoryItem>();
//            }

//            if (inventoryManager.InventoryBag_SO.itemList.Count == 0)
//            {
//                for (int i = 0; i < defaultBagSize; i++)
//                {
//                    inventoryManager.InventoryBag_SO.itemList.Add(new InventoryItem
//                    {
//                        ItemID = 0,
//                        ItemAmount = 0
//                    });
//                }
//            }


//            if (inventoryManager.MoneyDataList_SO.MoneyList.Count == 0)
//            {
//                inventoryManager.MoneyDataList_SO.MoneyList.Add(new Money
//                {
//                    money = defaultMoney
//                });
               
//            }

//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, inventoryManager.InventoryBag_SO.itemList);
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"初始化库存数据时出错: {e.Message}");
//        }
//    }

//    [ContextMenu("手动重新初始化")]
//    public void ManualReinitialize()
//    {
//        InitializeInventoryData();
//    }
//}