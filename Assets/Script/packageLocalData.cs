using System.Collections;
using System.Collections.Generic;
using System.IO;
using T.Inventory;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class InventoryBagData
{
    public List<InventoryItem> itemList;

    public InventoryBagData()
    {
        itemList = new List<InventoryItem>();
    }

    public InventoryBagData(InventoryBag_SO bagSO)
    {
        itemList = new List<InventoryItem>(bagSO.itemList);
    }
}

public class PackageLocalData 
{
    private static PackageLocalData _instance;

    public static PackageLocalData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PackageLocalData();
            }
            return _instance;
        }
    }
    public ItemDataList_SO ItemData;
    public InventoryBag_SO BagData;

    public void SavePackage(ItemDataList_SO ItemData)
    {
        string inventoryJson = JsonUtility.ToJson(ItemData);

        string filePath = "D:/yx/file.json";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(inventoryJson);
        }
    }
    public ItemDataList_SO LoadPackage(string Baglist)
    {

        if (File.Exists(Baglist))//检查路径是否存在
        {
            using (StreamReader reader = new StreamReader(Baglist))
            {
                string inventoryJson = reader.ReadToEnd();//读取文本中所有内容
                ItemData = JsonUtility.FromJson<ItemDataList_SO>(inventoryJson);
                return ItemData;
            }
        }
        else
        {
            ItemData = new ItemDataList_SO();
            return ItemData;
        }
    }

    //public void SaveBag(InventoryBag_SO BagData)
    //{
    //    string inventoryJson = JsonUtility.ToJson(BagData);

    //    string filePath = "D:/yx/file1.json";

    //    using (StreamWriter writer = new StreamWriter(filePath))
    //    {
    //        writer.Write(inventoryJson);
    //    }
    //}

    //public InventoryBag_SO LoadBag(string Baglist)
    //{

    //    if (File.Exists(Baglist))//检查路径是否存在
    //    {
    //        using (StreamReader reader = new StreamReader(Baglist))
    //        {
    //            string inventoryJson = reader.ReadToEnd();//读取文本中所有内容
    //            BagData = JsonUtility.FromJson<InventoryBag_SO>(inventoryJson);
    //            return BagData;

    //        }
    //    }
    //    else
    //    {
    //        BagData = new InventoryBag_SO();
    //        return BagData;
    //    }
    //}
    public void SaveBag(InventoryBag_SO BagData)
    {
        // 使用数据类进行序列化
        InventoryBagData bagData = new InventoryBagData(BagData);
        string inventoryJson = JsonUtility.ToJson(bagData);

        string filePath = "D:/yx/file1.json";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(inventoryJson);
        }
    }

    public InventoryBag_SO LoadBag(string Baglist)
    {
        if (File.Exists(Baglist))
        {
            using (StreamReader reader = new StreamReader(Baglist))
            {
                string inventoryJson = reader.ReadToEnd();

                // 反序列化到数据类
                InventoryBagData bagData = JsonUtility.FromJson<InventoryBagData>(inventoryJson);

                // 将数据复制到ScriptableObject
                if (BagData == null)
                {
                    BagData = ScriptableObject.CreateInstance<InventoryBag_SO>();
                }

                BagData.itemList = bagData?.itemList ?? new List<InventoryItem>();
                return BagData;
            }
        }
        else
        {
            if (BagData == null)
            {
                BagData = ScriptableObject.CreateInstance<InventoryBag_SO>();
            }
            BagData.itemList = new List<InventoryItem>();
            return BagData;
        }
    }
}