//using UnityEngine;
//using System.Collections;
//using T.Inventory;

//public class MySQLBootstrapper : MonoBehaviour
//{
//    [Header("MySQL设置")]
//    public bool loadOnStart = true;
//    public float loadDelay = 1.0f;

//    [Header("数据库连接配置")]
//    public string server = "localhost";
//    public string database = "gamedb";
//    public string username = "gameser";
//    public string password = "gamepassword";
//    public string port = "3306";

//    private void Awake()
//    {
//        // 确保MySQL管理器存在
//        if (MySQLInventoryManager.Instance == null)
//        {
//            var managerObject = new GameObject("MySQLInventoryManager");
//            managerObject.AddComponent<MySQLInventoryManager>();
//        }

//        // 配置数据库连接
//        var mysqlManager = MySQLInventoryManager.Instance;
//        mysqlManager.ConfigureDatabase(server, database, username, password, port);
//    }

//    private void Start()
//    {
//        if (loadOnStart)
//        {
//            StartCoroutine(DelayedLoad());
//        }
//    }

//    private IEnumerator DelayedLoad()
//    {
//        yield return new WaitForSeconds(loadDelay);
//        LoadGameData();
//    }

//    private void LoadGameData()
//    {
//        try
//        {
//            var inventoryManager = InventoryManager.Instance;
//            if (inventoryManager.InventoryBag_SO == null)
//            {
//                Debug.LogWarning("InventoryBag_SO 为 null");
//                return;
//            }

//            // 从MySQL加载背包数据
//            var inventoryData = MySQLInventoryManager.Instance.LoadInventoryData();
//            inventoryManager.InventoryBag_SO.itemList = inventoryData;

//            // 更新UI
//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, inventoryData);

//            // 加载金钱数据
//            if (inventoryManager.MoneyDataList_SO == null)
//            {
//                Debug.LogWarning("MoneyDataList_SO 为 null");
//                return;
//            }

//            int moneyAmount = MySQLInventoryManager.Instance.LoadMoneyData();

//            // 确保MoneyList不为空
//            inventoryManager.MoneyDataList_SO.MoneyList ??= new System.Collections.Generic.List<Money>();

//            if (inventoryManager.MoneyDataList_SO.MoneyList.Count == 0)
//                inventoryManager.MoneyDataList_SO.MoneyList.Add(new Money { money = moneyAmount });
//            else
//                inventoryManager.MoneyDataList_SO.MoneyList[0] = new Money { money = moneyAmount };

//            Debug.Log("游戏数据从MySQL加载完成");
//        }
//        catch (System.Exception e)
//        {
//            Debug.LogError($"加载游戏数据失败: {e.Message}");
//        }
//    }

//    // 手动保存
//    [ContextMenu("手动保存")]
//    public void ManualSave()
//    {
//        if (MySQLInventoryManager.Instance != null)
//        {
//            MySQLInventoryManager.Instance.SaveCurrentGameData();
//            Debug.Log("手动保存完成");
//        }
//    }

//    // 手动加载
//    [ContextMenu("手动加载")]
//    public void ManualLoad()
//    {
//        LoadGameData();
//        Debug.Log("手动加载完成");
//    }
//}
