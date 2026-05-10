using UnityEngine;
using System.Collections;
using T.Inventory;
using static StorageFactory;

public class StorageOptions : MonoBehaviour
{
    [Header("¥Ê¥¢…Ë÷√")]
    public StorageType storageType = StorageType.SQLite;
    public bool loadOnStart = true;
    public float loadDelay = 1.0f;

    [Header("MySQL≈‰÷√")]
    public MySqlData mySQL = new MySqlData();

    private StorageInterface storageInterface;
    private StorageManagerFactory currentFactory;

    private void Awake()
    {
        InitializeStorageManager();
    }

    private void InitializeStorageManager()
    {

        currentFactory = StorageFactoryCreate.CreateFactory(
            storageType,
            transform,
            storageType == StorageType.MySQL ? mySQL : null
        );


        storageInterface = currentFactory.CreateStorageManager();

    }

    private void Start()
    {
        if (loadOnStart)
        {
            StartCoroutine(DelayedLoad());
        }
    }

    private IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(loadDelay);
        LoadGameData();
    }

    private void LoadGameData()
    {
        try
        {
            var inventoryManager = InventoryManager.Instance;

            var inventoryData = storageInterface.LoadInventoryData();
            inventoryManager.InventoryBag_SO.itemList = inventoryData;

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, inventoryData);


            int moneyAmount = storageInterface.LoadMoneyData();

            inventoryManager.MoneyDataList_SO.MoneyList ??= new System.Collections.Generic.List<Money>();

            if (inventoryManager.MoneyDataList_SO.MoneyList.Count == 0)
                inventoryManager.MoneyDataList_SO.MoneyList.Add(new Money { money = moneyAmount });
            else
                inventoryManager.MoneyDataList_SO.MoneyList[0] = new Money { money = moneyAmount };

        }
        catch (System.Exception e)
        {
            Debug.LogError($"º”‘ÿ”Œœ∑ ˝æ› ß∞‹: {e.Message}");
        }
    }


    private void OnApplicationQuit()
    {
        storageInterface?.SaveCurrentGameData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            storageInterface?.SaveCurrentGameData();
        }
    }
}