//using UnityEngine;
//using System.Collections;
//using T.Inventory;

//public class SQLiteBootstrapper : MonoBehaviour
//{
//    [Header("设置")]
//    public bool loadStart = true;
//    public float loadTime = 1.0f;

//    private void Awake()
//    {
//        if (SQLiteInventoryManager.Instance == null)
//        {
//            var managerObject = new GameObject("SQLiteInventoryManager");
//            managerObject.AddComponent<SQLiteInventoryManager>();
//        }
//    }

//    private void Start()
//    {
//        if (loadStart)
//        {
//            StartCoroutine(DelayedLoad());
//        }
//    }

//    private IEnumerator DelayedLoad()
//    {
//        yield return new WaitForSeconds(loadTime);
//        LoadGameData();
//    }

//    private void LoadGameData()
//    {
//        try
//        {
//            var inventoryManager = InventoryManager.Instance;
//            // 加载背包数据
//            var inventoryData = SQLiteInventoryManager.Instance.LoadInventoryData();
//            inventoryManager.InventoryBag_SO.itemList = inventoryData;

//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, inventoryData);

//            // 加载金钱数据
//            //if (inventoryManager.MoneyDataList_SO == null)
//            //{
//            //    Debug.LogWarning("MoneyDataList_SO 为 null");
//            //    return;
//            //}
//            int moneyAmount = SQLiteInventoryManager.Instance.LoadMoneyData();

//            inventoryManager.MoneyDataList_SO.MoneyList ??= new System.Collections.Generic.List<Money>();

//            if (inventoryManager.MoneyDataList_SO.MoneyList.Count == 0)
//                inventoryManager.MoneyDataList_SO.MoneyList.Add(new Money { money = moneyAmount });
//            else
//                inventoryManager.MoneyDataList_SO.MoneyList[0] = new Money { money = moneyAmount };

//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"加载游戏数据失败: {e.Message}");
//        }
//    }

//    // 手动保存
//    public void ManualSave()
//    {
//        if (SQLiteInventoryManager.Instance != null)
//        {
//            SQLiteInventoryManager.Instance.SaveCurrentGameData();
//        }
//    }

//    // 手动加载
//    public void ManualLoad()
//    {
//        LoadGameData();
//    }
//}