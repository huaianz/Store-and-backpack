////using System;
////using System.Collections;
////using System.Collections.Generic;
////using System.IO;
////using UnityEditor.U2D.Aseprite;
////using UnityEngine;
////using UnityEngine.Events;


////namespace T.Inventory
////{
////    [System.Serializable]
////    public class InventoryManager : Singleton<InventoryManager>
////    {
////        [Header("物品数据")]
////        public ItemDataList_SO ItemDataList_SO;
////        [Header("背包数据")]
////        public InventoryBag_SO InventoryBag_SO;
////        [Header("商店数据")]
////        public StroeList_SO stroeList_SO;
////        [Header("金币数据")]
////        public MoneyDataList_SO MoneyDataList_SO;



////        string filePath = "D:/yx/file.json";
////        string filePath1 = "D:/yx/file1.json";

////        private void OnEnable()
////        {
////            //EventHandler.DropItemEvent += OnDropItemEvent;
////            //EventHandler.InstantiateItemInScene += OmInstanteItemInScene;
////        }


////        private void OnDisable()
////        {
////            //EventHandler.DropItemEvent -=OnDropItemEvent;
////            //EventHandler.InstantiateItemInScene -= OmInstanteItemInScene;
////        }


////        private void Start()
////        {
////            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
////            EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);

////            //PackageLocalData.Instance.SaveBag(InventoryBag_SO);
////            //PackageLocalData.Instance.SavePackage(ItemDataList_SO);
////            //if (PackageLocalData.Instance.LoadPackage(filePath) != null)
////            //{
////            //    ItemDataList_SO = PackageLocalData.Instance.LoadPackage(filePath);
////            //}


////        }


////        private void Update()
////        {

////            if (Input.GetMouseButtonDown(0))
////            {
////                print("itemComponent.ItemID");
////                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
////                RaycastHit hit;

////                if (Physics.Raycast(ray, out hit))
////                {
////                    if (hit.collider.CompareTag("item"))
////                    {
////                        Item itemComponent = hit.collider.gameObject.GetComponent<Item>();


////                        if (itemComponent != null)
////                        {
////                            InventoryManager.Instance.AddItem(itemComponent.ItemID);
////                            print(itemComponent.ItemID);
////                            Destroy(itemComponent.gameObject);
////                        }
////                    }

////                }
////            }
////            if (Input.GetKeyDown(KeyCode.A))
////            {
////                SaveBag(InventoryBag_SO);
////            }
////            if (Input.GetKeyUp(KeyCode.B))
////            {
////                InventoryBag_SO = LoadBag(filePath1);
////            }
////        }

////        public ItemDetails GetItemDetails(int ID)//返回物品ID
////        {
////            if (ItemDataList_SO == null)
////            {
////                return null;
////            }
////            return ItemDataList_SO.itemDetailsList.Find(i => i.ItemID == ID);
////        }


////        //捡起物品
////        public void AddItem(int ID)//添加物品到背包和销毁
////        {
////            var index = GetItemIndexInBag(ID);
////            int currentAmount = InventoryBag_SO.itemList[index].ItemAmount + 1;
////            if (currentAmount > 99)
////            {
////                var item1 = new InventoryItem { ItemID = ID, ItemAmount = currentAmount - 99 };
////                for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////                {
////                    if (InventoryBag_SO.itemList[i].ItemID == 0)
////                    {
////                        InventoryBag_SO.itemList[i] = item1;
////                        break;
////                    }
////                }
////                var item2 = new InventoryItem { ItemID = ID, ItemAmount = 99 };
////                InventoryBag_SO.itemList[index] = item2;
////            }
////            else
////            {
////                var item = new InventoryItem { ItemID = ID, ItemAmount = currentAmount };
////                InventoryBag_SO.itemList[index] = item;
////            }
////            //AddItemAtIndex(ID, index, 1);

////            //减少金额UI


////            //if (toDestory)
////            //{
////            //    Destroy(item.gameObject);
////            //}
////            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
////        }

////        private int GetItemIndexInBag(int ID)//检查背包里该物品的位置
////        {
////            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////            {
////                if (InventoryBag_SO.itemList[i].ItemID == ID)
////                    return i;
////            }
////            return -1;
////        }

////        private bool backpackvacancy()//检查是否有这个物品
////        {
////            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////            {
////                if (InventoryBag_SO.itemList[i].ItemID == 0)
////                    return true;
////            }
////            return false;
////        }

////        private void AddItemAtIndex(int ID, int index, int amount)
////        {
////            if (index == -1 && backpackvacancy())//没有这个物品
////            {
////                if (amount > 99)
////                {
////                    var item1 = new InventoryItem { ItemID = ID, ItemAmount = amount - 99 };
////                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////                    {
////                        if (InventoryBag_SO.itemList[i].ItemID == 0)
////                        {
////                            InventoryBag_SO.itemList[i] = item1;
////                            break;
////                        }
////                    }
////                    var item = new InventoryItem { ItemID = ID, ItemAmount = 99 };
////                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////                    {
////                        if (InventoryBag_SO.itemList[i].ItemID == 0)
////                        {
////                            InventoryBag_SO.itemList[i] = item;
////                            break;
////                        }
////                    }
////                }
////                else
////                {
////                    var item = new InventoryItem { ItemID = ID, ItemAmount = amount };
////                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////                    {
////                        if (InventoryBag_SO.itemList[i].ItemID == 0)
////                        {
////                            InventoryBag_SO.itemList[i] = item;
////                            break;
////                        }
////                    }
////                }
////            }
////            else
////            {
////                //数量不能超过99
////                int currentAmount = InventoryBag_SO.itemList[index].ItemAmount + amount;
////                if (currentAmount > 99)
////                {
////                    var item1 = new InventoryItem { ItemID = ID, ItemAmount = currentAmount - 99 };
////                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
////                    {
////                        if (InventoryBag_SO.itemList[i].ItemID == 0)
////                        {
////                            InventoryBag_SO.itemList[i] = item1;
////                            break;
////                        }
////                    }
////                    var item2 = new InventoryItem { ItemID = ID, ItemAmount = 99 };
////                    InventoryBag_SO.itemList[index] = item2;
////                }
////                else
////                {
////                    var item = new InventoryItem { ItemID = ID, ItemAmount = currentAmount };
////                    InventoryBag_SO.itemList[index] = item;
////                }
////            }
////        }


////        /// <summary>
////        /// 背包内交换物品
////        /// </summary>
////        /// <param name="fromIndex"></param>
////        /// <param name="targetIndex"></param>
////        public void SwapItem(int fromIndex, int targetIndex)
////        {
////            InventoryItem currentItem = InventoryBag_SO.itemList[fromIndex];
////            InventoryItem targetItem = InventoryBag_SO.itemList[targetIndex];

////            if (targetItem.ItemID != 0)
////            {
////                InventoryBag_SO.itemList[fromIndex] = targetItem;
////                InventoryBag_SO.itemList[targetIndex] = currentItem;
////            }
////            else
////            {
////                InventoryBag_SO.itemList[targetIndex] = currentItem;
////                InventoryBag_SO.itemList[fromIndex] = new InventoryItem();
////            }
////            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
////            //监听事件改变UI
////        }

////        //售卖背包内的物品
////        public void SellItem(int itemID)
////        {
////            var index = GetItemIndexInBag(itemID);

////            if (InventoryBag_SO.itemList[index].ItemAmount >= 1)
////            {
////                var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
////                var item = new InventoryItem { ItemID = itemID, ItemAmount = amount };
////                InventoryBag_SO.itemList[index] = item;
////                if (amount == 0)
////                {
////                    var item1 = new InventoryItem { ItemID = 0, ItemAmount = 0 };
////                    InventoryBag_SO.itemList[index] = item1;
////                }
////                //增加钱额UI
////            }
////            else if (InventoryBag_SO.itemList[index].ItemAmount == 0)
////            {
////                var item = new InventoryItem();
////                InventoryBag_SO.itemList[index] = item;
////            }
////        }

////        //交易
////        public void TradeItem(ItemDetails itemDetails)
////        {
////            int cost = itemDetails.ItemPrice;
////            int index = GetItemIndexInBag(itemDetails.ItemID);

////            //卖
////            if (InventoryBag_SO.itemList[index].ItemAmount >= 1)
////            {
////                SellItem(itemDetails.ItemID);
////                //金额
////                int moneyItem = MoneyDataList_SO.MoneyList[0].money;
////                //moneyItem.money += cost
////                var item = new Money { money = moneyItem + cost };
////                MoneyDataList_SO.MoneyList[0] = item;
////            }
////            else
////            {
////                return;
////            }

////            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
////        }

////        public void Buy(ItemDetails itemDetails)
////        {
////            int cost = itemDetails.ItemPrice;
////            int index = GetItemIndexInBag(itemDetails.ItemID);
////            if (MoneyDataList_SO.MoneyList[0].money >= cost)
////            {
////                AddItemAtIndex(itemDetails.ItemID, index, 1);
////                int moneyItem = MoneyDataList_SO.MoneyList[0].money;
////                var item = new Money { money = moneyItem - cost };
////                MoneyDataList_SO.MoneyList[0] = item;
////            }
////            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);

////        }
////        public void Reduce(int ID)
////        {
////            var index = GetItemIndexInBag(ID);
////            var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
////            if (amount != 0)
////            {
////                var item = new InventoryItem { ItemID = ID, ItemAmount = amount };
////                InventoryBag_SO.itemList[index] = item;
////            }
////            else
////            {
////                var item = new InventoryItem { ItemID = 0, ItemAmount = 0 };
////                InventoryBag_SO.itemList[index] = item;
////            }

////            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
////        }
////        //保存
////        public void SavePackage(ItemDataList_SO ItemData)
////        {
////            string inventoryJson = JsonUtility.ToJson(ItemData);

////            string filePath = "D:/yx/file.json";

////            using (StreamWriter writer = new StreamWriter(filePath))
////            {
////                writer.Write(inventoryJson);
////            }
////        }
////        public ItemDataList_SO LoadPackage(string Baglist)
////        {
////            ItemDataList_SO ItemData;
////            if (File.Exists(Baglist))//检查路径是否存在
////            {
////                using (StreamReader reader = new StreamReader(Baglist))
////                {
////                    string inventoryJson = reader.ReadToEnd();//读取文本中所有内容
////                                                              //PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);
////                    ItemData = JsonUtility.FromJson<ItemDataList_SO>(inventoryJson);
////                    return ItemData;
////                }
////            }
////            else
////            {
////                ItemData = new ItemDataList_SO();
////                return ItemData;
////            }
////        }
////        public void SaveBag(InventoryBag_SO BagData)
////        {
////            string inventoryJson = JsonUtility.ToJson(BagData);

////            string filePath = "D:/yx/file1.json";

////            using (StreamWriter writer = new StreamWriter(filePath))
////            {
////                writer.Write(inventoryJson);
////            }
////        }

////        public InventoryBag_SO LoadBag(string Baglist)
////        {
////            InventoryBag_SO BagData;
////            if (File.Exists(Baglist))//检查路径是否存在
////            {
////                using (StreamReader reader = new StreamReader(Baglist))
////                {
////                    string inventoryJson = reader.ReadToEnd();//读取文本中所有内容
////                                                              //PackageLocalData packageLocalData = JsonUtility.FromJson<PackageLocalData>(inventoryJson);

////                    BagData = JsonUtility.FromJson<InventoryBag_SO>(inventoryJson);
////                    return BagData;
////                }
////            }
////            else
////            {
////                BagData = new InventoryBag_SO();
////                return BagData;
////            }
////        }
////    }
////}

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.Events;

//namespace T.Inventory
//{
//    [System.Serializable]
//    public class InventoryManager : Singleton<InventoryManager>
//    {
//        [Header("物品数据")]
//        public ItemDataList_SO ItemDataList_SO;
//        [Header("背包数据")]
//        public InventoryBag_SO InventoryBag_SO;
//        [Header("商店数据")]
//        public StroeList_SO stroeList_SO;
//        [Header("金币数据")]
//        public MoneyDataList_SO MoneyDataList_SO;


//        string filePath = "D:/yx/file.json";
//        string filePath1 = "D:/yx/file1.json";
//        private void OnEnable()
//        {
//            //EventHandler.DropItemEvent += OnDropItemEvent;
//            //EventHandler.InstantiateItemInScene += OmInstanteItemInScene;
//        }

//        private void OnDisable()
//        {
//            //EventHandler.DropItemEvent -= OnDropItemEvent;
//            //EventHandler.InstantiateItemInScene -= OmInstanteItemInScene;
//        }

//        private void Start()
//        {
//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//            EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);

//        }

//        private void Update()
//        {

//            if (Input.GetMouseButtonDown(0))
//            {

//                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//                RaycastHit hit;

//                if (Physics.Raycast(ray, out hit))
//                {
//                    if (hit.collider.CompareTag("item"))
//                    {
//                        Item itemComponent = hit.collider.gameObject.GetComponent<Item>();

//                        if (itemComponent != null)
//                        {
//                            InventoryManager.Instance.AddItem(itemComponent.ItemID);
//                            print(itemComponent.ItemID);
//                            Destroy(itemComponent.gameObject);
//                        }
//                    }
//                }
//            }
//            if (Input.GetKeyDown(KeyCode.Z))
//            {
//                PackageLocalData.Instance.SaveBag(InventoryBag_SO);
//            }
//            if (Input.GetKeyUp(KeyCode.C))
//            {
//                InventoryBag_SO = PackageLocalData.Instance.LoadBag(filePath1);
//                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//            }


//        }

//        public ItemDetails GetItemDetails(int ID) // 返回物品ID
//        {
//            if (ItemDataList_SO == null)
//            {
//                return null;
//            }
//            return ItemDataList_SO.itemDetailsList.Find(i => i.ItemID == ID);
//        }

//        // 捡起物品
//        public void AddItem(int ID) // 添加物品到背包和销毁
//        {
//            var index = GetItemIndexInBag(ID);
//            int currentAmount = InventoryBag_SO.itemList[index].ItemAmount + 1;
//            if (currentAmount > 99)
//            {
//                var item1 = new InventoryItem { ItemID = ID, ItemAmount = currentAmount - 99 };
//                for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//                {
//                    if (InventoryBag_SO.itemList[i].ItemID == 0)
//                    {
//                        InventoryBag_SO.itemList[i] = item1;
//                        break;
//                    }
//                }
//                var item2 = new InventoryItem { ItemID = ID, ItemAmount = 99 };
//                InventoryBag_SO.itemList[index] = item2;
//            }
//            else
//            {
//                var item = new InventoryItem { ItemID = ID, ItemAmount = currentAmount };
//                InventoryBag_SO.itemList[index] = item;
//            }
//            //AddItemAtIndex(ID, index, 1);

//            //减少金额UI

//            //if (toDestory)
//            //{
//            //    Destroy(item.gameObject);
//            //}
//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//        }

//        private int GetItemIndexInBag(int ID) // 检查背包里该物品的位置
//        {
//            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//            {
//                if (InventoryBag_SO.itemList[i].ItemID == ID)
//                    return i;
//            }
//            return -1;
//        }

//        private bool backpackvacancy() // 检查是否有这个物品
//        {
//            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//            {
//                if (InventoryBag_SO.itemList[i].ItemID == 0)
//                    return true;
//            }
//            return false;
//        }

//        private void AddItemAtIndex(int ID, int index, int amount)
//        {
//            if (index == -1 && backpackvacancy()) // 没有这个物品
//            {
//                if (amount > 99)
//                {
//                    var item1 = new InventoryItem { ItemID = ID, ItemAmount = amount - 99 };
//                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//                    {
//                        if (InventoryBag_SO.itemList[i].ItemID == 0)
//                        {
//                            InventoryBag_SO.itemList[i] = item1;
//                            break;
//                        }
//                    }
//                    var item = new InventoryItem { ItemID = ID, ItemAmount = 99 };
//                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//                    {
//                        if (InventoryBag_SO.itemList[i].ItemID == 0)
//                        {
//                            InventoryBag_SO.itemList[i] = item;
//                            break;
//                        }
//                    }
//                }
//                else
//                {
//                    var item = new InventoryItem { ItemID = ID, ItemAmount = amount };
//                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//                    {
//                        if (InventoryBag_SO.itemList[i].ItemID == 0)
//                        {
//                            InventoryBag_SO.itemList[i] = item;
//                            break;
//                        }
//                    }
//                }
//            }
//            else
//            {
//                // 数量不能超过99
//                int currentAmount = InventoryBag_SO.itemList[index].ItemAmount + amount;
//                if (currentAmount > 99)
//                {
//                    var item1 = new InventoryItem { ItemID = ID, ItemAmount = currentAmount - 99 };
//                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//                    {
//                        if (InventoryBag_SO.itemList[i].ItemID == 0)
//                        {
//                            InventoryBag_SO.itemList[i] = item1;
//                            break;
//                        }
//                    }
//                    var item2 = new InventoryItem { ItemID = ID, ItemAmount = 99 };
//                    InventoryBag_SO.itemList[index] = item2;
//                }
//                else
//                {
//                    var item = new InventoryItem { ItemID = ID, ItemAmount = currentAmount };
//                    InventoryBag_SO.itemList[index] = item;
//                }
//            }
//        }

//        /// <summary>
//        /// 背包内交换物品
//        /// </summary>
//        /// <param name="fromIndex"></param>
//        /// <param name="targetIndex"></param>
//        public void SwapItem(int fromIndex, int targetIndex)
//        {
//            InventoryItem currentItem = InventoryBag_SO.itemList[fromIndex];
//            InventoryItem targetItem = InventoryBag_SO.itemList[targetIndex];

//            if (targetItem.ItemID != 0)
//            {
//                InventoryBag_SO.itemList[fromIndex] = targetItem;
//                InventoryBag_SO.itemList[targetIndex] = currentItem;
//            }
//            else
//            {
//                InventoryBag_SO.itemList[targetIndex] = currentItem;
//                InventoryBag_SO.itemList[fromIndex] = new InventoryItem();
//            }
//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//            // 监听事件改变UI
//        }

//        // 售卖背包内的物品
//        public void SellItem(int itemID)
//        {
//            var index = GetItemIndexInBag(itemID);

//            if (InventoryBag_SO.itemList[index].ItemAmount >= 1)


//            {
//                var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
//                var item = new InventoryItem { ItemID = itemID, ItemAmount = amount };
//                InventoryBag_SO.itemList[index] = item;
//                if (amount == 0)
//                {
//                    var item1 = new InventoryItem { ItemID = 0, ItemAmount = 0 };
//                    InventoryBag_SO.itemList[index] = item1;
//                }
//                // 增加钱额UI
//            }
//            else if (InventoryBag_SO.itemList[index].ItemAmount == 0)
//            {
//                var item = new InventoryItem();
//                InventoryBag_SO.itemList[index] = item;
//            }
//        }    // 交易
//        public void TradeItem(ItemDetails itemDetails)
//        {
//            int cost = itemDetails.ItemPrice;
//            int index = GetItemIndexInBag(itemDetails.ItemID);

//            // 卖
//            if (InventoryBag_SO.itemList[index].ItemAmount >= 1)
//            {
//                SellItem(itemDetails.ItemID);
//                // 金额
//                int moneyItem = MoneyDataList_SO.MoneyList[0].money;
//                // moneyItem.money += cost
//                var item = new Money { money = moneyItem + cost };
//                MoneyDataList_SO.MoneyList[0] = item;
//            }
//            else
//            {
//                return;
//            }

//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//        }

//        public void Buy(ItemDetails itemDetails)
//        {
//            int cost = itemDetails.ItemPrice;
//            int index = GetItemIndexInBag(itemDetails.ItemID);
//            if (MoneyDataList_SO.MoneyList[0].money >= cost)
//            {
//                AddItemAtIndex(itemDetails.ItemID, index, 1);
//                int moneyItem = MoneyDataList_SO.MoneyList[0].money;
//                var item = new Money { money = moneyItem - cost };
//                MoneyDataList_SO.MoneyList[0] = item;
//            }
//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//        }

//        public void Reduce(int ID)
//        {
//            var index = GetItemIndexInBag(ID);
//            var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
//            if (amount != 0)
//            {
//                var item = new InventoryItem { ItemID = ID, ItemAmount = amount };
//                InventoryBag_SO.itemList[index] = item;
//            }
//            else
//            {
//                var item = new InventoryItem { ItemID = 0, ItemAmount = 0 };
//                InventoryBag_SO.itemList[index] = item;
//            }

//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//        }
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace T.Inventory
//{
//    public class InventoryManager : Singleton<InventoryManager>
//    {
//        [Header("物品数据")]
//        public ItemDataList_SO ItemDataList_SO;
//        [Header("背包数据")]
//        public InventoryBag_SO InventoryBag_SO;
//        [Header("商店数据")]
//        public StroeList_SO stroeList_SO;
//        [Header("金钱数据")]
//        public MoneyDataList_SO MoneyDataList_SO;

//        private void Start()
//        {
//            if (SocketManager.Instance != null)
//            {
//                SocketManager.Instance.ConnectToServer();
//                StartCoroutine(LoadAllDataFromServer());
//            }
//            else
//            {
//                Debug.LogError("SocketManager实例未找到");
//                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//                EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);
//            }
//        }

//        private IEnumerator LoadAllDataFromServer()
//        {
//            yield return new WaitForSeconds(0.5f);

//            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
//            {
//                SocketManager.Instance.SendMessage("GET_ITEMS", "");
//                yield return new WaitForSeconds(0.1f);

//                SocketManager.Instance.SendMessage("GET_INVENTORY", "");
//                yield return new WaitForSeconds(0.1f);

//                SocketManager.Instance.SendMessage("GET_STORE_ITEMS", "");
//                yield return new WaitForSeconds(0.1f);

//                SocketManager.Instance.SendMessage("GET_MONEY", "");
//            }
//            else
//            {
//                Debug.LogWarning("无法连接到服务器，使用本地数据");
//                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//                EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);
//            }
//        }

//        // 数据接收回调方法
//        public void OnItemsReceived(List<ServerItemDetails> serverItems)
//        {
//            if (ItemDataList_SO.itemDetailsList == null)
//                ItemDataList_SO.itemDetailsList = new List<ItemDetails>();
//            else
//                ItemDataList_SO.itemDetailsList.Clear();

//            foreach (var serverItem in serverItems)
//            {
//                var localItem = new ItemDetails
//                {
//                    ItemID = serverItem.ItemID,
//                    ItemName = serverItem.ItemName,
//                    ItemPrice = serverItem.ItemPrice,
//                    ItemSpriteName = serverItem.ItemSpriteName
//                };

//                ItemDataList_SO.itemDetailsList.Add(localItem);
//            }

//            Debug.Log($"接收到 {serverItems.Count} 个物品");
//        }

//        public void OnInventoryReceived(List<ServerInventoryItem> serverInventory)
//        {
//            if (InventoryBag_SO.itemList == null)
//                InventoryBag_SO.itemList = new List<InventoryItem>();
//            else
//                InventoryBag_SO.itemList.Clear();

//            foreach (var serverItem in serverInventory)
//            {
//                InventoryBag_SO.itemList.Add(new InventoryItem
//                {
//                    SlotIndex = serverItem.SlotIndex,
//                    ItemID = serverItem.ItemID,
//                    ItemAmount = serverItem.ItemAmount
//                });
//            }

//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//            Debug.Log($"接收到背包数据，共 {serverInventory.Count} 个格子");
//        }

//        public void OnStoreItemsReceived(List<ServerStoreItem> serverStoreItems)
//        {
//            if (stroeList_SO.Stroelist == null)
//                stroeList_SO.Stroelist = new List<Store>();
//            else
//                stroeList_SO.Stroelist.Clear();

//            foreach (var serverItem in serverStoreItems)
//            {
//                stroeList_SO.Stroelist.Add(new Store
//                {
//                    ItemID = serverItem.ItemID,
//                    ItemPrice = serverItem.StorePrice
//                });
//            }

//            EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);
//            Debug.Log($"接收到 {serverStoreItems.Count} 个商店物品");
//        }

//        public void OnMoneyReceived(ServerPlayerMoney money)
//        {
//            if (MoneyDataList_SO.MoneyList == null)
//                MoneyDataList_SO.MoneyList = new List<Money>();

//            if (MoneyDataList_SO.MoneyList.Count > 0)
//            {
//                MoneyDataList_SO.MoneyList[0].money = money.MoneyAmount;
//            }
//            else
//            {
//                MoneyDataList_SO.MoneyList.Add(new Money { money = money.MoneyAmount });
//            }
//            Debug.Log($"接收到金钱数据: {money.MoneyAmount}");
//        }

//        // 交易相关方法
//        public void Buy(ItemDetails itemDetails)
//        {
//            StartCoroutine(ProcessBuy(itemDetails));
//        }

//        public void TradeItem(ItemDetails itemDetails)
//        {
//            StartCoroutine(ProcessSell(itemDetails));
//        }

//        private IEnumerator ProcessBuy(ItemDetails itemDetails)
//        {
//            int cost = itemDetails.ItemPrice;
//            int playerMoney = MoneyDataList_SO.MoneyList[0].money;

//            if (playerMoney >= cost)
//            {
//                yield return StartCoroutine(UpdateMoneyOnServer(playerMoney - cost));
//                yield return StartCoroutine(AddItemToServer(itemDetails.ItemID));
//                Debug.Log($"成功购买物品: {itemDetails.ItemName}");
//            }
//            else
//            {
//                Debug.LogWarning("金钱不足，无法购买");
//            }
//        }

//        private IEnumerator ProcessSell(ItemDetails itemDetails)
//        {
//            int sellPrice = itemDetails.ItemPrice;
//            int playerMoney = MoneyDataList_SO.MoneyList[0].money;
//            var index = GetItemIndexInBag(itemDetails.ItemID);

//            if (index != -1 && InventoryBag_SO.itemList[index].ItemAmount > 0)
//            {
//                yield return StartCoroutine(ReduceItemOnServer(itemDetails.ItemID));
//                yield return StartCoroutine(UpdateMoneyOnServer(playerMoney + sellPrice));
//                Debug.Log($"成功出售物品: {itemDetails.ItemName}");
//            }
//            else
//            {
//                Debug.LogWarning("物品不存在或数量不足，无法出售");
//            }
//        }

//        // 网络操作方法
//        private IEnumerator UpdateMoneyOnServer(int newAmount)
//        {
//            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
//            {
//                var updateData = new { amount = newAmount };
//                string jsonData = JsonUtility.ToJson(updateData);

//                bool updateCompleted = false;
//                SocketManager.Instance.SendMessage("UPDATE_MONEY", jsonData, (response) => {
//                    updateCompleted = true;
//                });

//                yield return new WaitUntil(() => updateCompleted);
//                MoneyDataList_SO.MoneyList[0].money = newAmount;
//            }
//        }

//        private IEnumerator AddItemToServer(int itemID)
//        {
//            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
//            {
//                var index = GetItemIndexInBag(itemID);
//                int currentAmount = index != -1 ? InventoryBag_SO.itemList[index].ItemAmount + 1 : 1;
//                int slotIndex = index != -1 ? index : FindEmptySlot();

//                if (slotIndex != -1)
//                {
//                    var updateData = new { slotIndex = slotIndex, itemId = itemID, amount = currentAmount };
//                    string jsonData = JsonUtility.ToJson(updateData);

//                    bool updateCompleted = false;
//                    SocketManager.Instance.SendMessage("UPDATE_INVENTORY", jsonData, (response) => {
//                        updateCompleted = true;
//                    });

//                    yield return new WaitUntil(() => updateCompleted);

//                    if (index != -1)
//                    {
//                        InventoryBag_SO.itemList[index].ItemAmount = currentAmount;
//                    }
//                    else
//                    {
//                        InventoryBag_SO.itemList[slotIndex] = new InventoryItem
//                        {
//                            SlotIndex = slotIndex,
//                            ItemID = itemID,
//                            ItemAmount = currentAmount
//                        };
//                    }

//                    EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//                }
//            }
//        }

//        private IEnumerator ReduceItemOnServer(int itemID)
//        {
//            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
//            {
//                var index = GetItemIndexInBag(itemID);
//                if (index != -1)
//                {
//                    int newAmount = InventoryBag_SO.itemList[index].ItemAmount - 1;

//                    var updateData = new { slotIndex = index, itemId = itemID, amount = newAmount };
//                    string jsonData = JsonUtility.ToJson(updateData);

//                    bool updateCompleted = false;
//                    SocketManager.Instance.SendMessage("UPDATE_INVENTORY", jsonData, (response) => {
//                        updateCompleted = true;
//                    });

//                    yield return new WaitUntil(() => updateCompleted);

//                    if (newAmount > 0)
//                    {
//                        InventoryBag_SO.itemList[index].ItemAmount = newAmount;
//                    }
//                    else
//                    {
//                        InventoryBag_SO.itemList[index] = new InventoryItem
//                        {
//                            SlotIndex = index,
//                            ItemID = 0,
//                            ItemAmount = 0
//                        };
//                    }

//                    EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//                }
//            }
//        }

//        // 辅助方法
//        private int GetItemIndexInBag(int ID)
//        {
//            if (InventoryBag_SO.itemList == null) return -1;

//            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//            {
//                if (InventoryBag_SO.itemList[i].ItemID == ID)
//                    return i;
//            }
//            return -1;
//        }

//        private int FindEmptySlot()
//        {
//            if (InventoryBag_SO.itemList == null) return -1;

//            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
//            {
//                if (InventoryBag_SO.itemList[i].ItemID == 0)
//                    return i;
//            }
//            return -1;
//        }

//        public ItemDetails GetItemDetails(int ID)
//        {
//            if (ItemDataList_SO == null || ItemDataList_SO.itemDetailsList == null) return null;
//            return ItemDataList_SO.itemDetailsList.Find(i => i.ItemID == ID);
//        }

//        // 原有方法
//        public void AddItem(int itemID)
//        {
//            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
//            {
//                var index = GetItemIndexInBag(itemID);
//                int currentAmount = index != -1 ? InventoryBag_SO.itemList[index].ItemAmount + 1 : 1;
//                int slotIndex = index != -1 ? index : FindEmptySlot();

//                if (slotIndex != -1)
//                {
//                    var updateData = new { slotIndex = slotIndex, itemId = itemID, amount = currentAmount };
//                    string jsonData = JsonUtility.ToJson(updateData);
//                    SocketManager.Instance.SendMessage("UPDATE_INVENTORY", jsonData);
//                }
//            }
//            else
//            {
//                var index = GetItemIndexInBag(itemID);
//                if (index != -1)
//                {
//                    InventoryBag_SO.itemList[index].ItemAmount += 1;
//                }
//                else
//                {
//                    int emptySlot = FindEmptySlot();
//                    if (emptySlot != -1)
//                    {
//                        InventoryBag_SO.itemList[emptySlot] = new InventoryItem
//                        {
//                            SlotIndex = emptySlot,
//                            ItemID = itemID,
//                            ItemAmount = 1
//                        };
//                    }
//                }
//                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//            }
//        }

//        public void SwapItem(int fromIndex, int targetIndex)
//        {
//            if (InventoryBag_SO.itemList == null) return;

//            InventoryItem currentItem = InventoryBag_SO.itemList[fromIndex];
//            InventoryItem targetItem = InventoryBag_SO.itemList[targetIndex];

//            if (targetItem.ItemID != 0)
//            {
//                InventoryBag_SO.itemList[fromIndex] = targetItem;
//                InventoryBag_SO.itemList[targetIndex] = currentItem;
//            }
//            else
//            {
//                InventoryBag_SO.itemList[targetIndex] = currentItem;
//                InventoryBag_SO.itemList[fromIndex] = new InventoryItem();
//            }
//            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//        }

//        public void Reduce(int ID)
//        {
//            var index = GetItemIndexInBag(ID);
//            if (index != -1)
//            {
//                var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
//                if (amount > 0)
//                {
//                    InventoryBag_SO.itemList[index].ItemAmount = amount;
//                }
//                else
//                {
//                    InventoryBag_SO.itemList[index] = new InventoryItem
//                    {
//                        SlotIndex = index,
//                        ItemID = 0,
//                        ItemAmount = 0
//                    };
//                }
//                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
//            }
//        }
//    }
//}

using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO ItemDataList_SO;
        [Header("背包数据")]
        public InventoryBag_SO InventoryBag_SO;
        [Header("商店数据")]
        public StroeList_SO stroeList_SO;
        [Header("金钱数据")]
        public MoneyDataList_SO MoneyDataList_SO;

        private void Start()
        {
            if (SocketManager.Instance != null)
            {
                SocketManager.Instance.ConnectToServer();
                StartCoroutine(LoadAllDataFromServer());
            }
            else
            {
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
                EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);
            }
        }
       

        private IEnumerator LoadAllDataFromServer()
        {
            yield return new WaitForSeconds(0.5f);

            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
            {
                SocketManager.Instance.SendMessage("GET_ITEMS", "");
                yield return new WaitForSeconds(0.1f);

                SocketManager.Instance.SendMessage("GET_INVENTORY", "");
                yield return new WaitForSeconds(0.1f);

                SocketManager.Instance.SendMessage("GET_STORE_ITEMS", "");
                yield return new WaitForSeconds(0.1f);

                SocketManager.Instance.SendMessage("GET_MONEY", "");
            }
            else
            {
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
                EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);
            }
        }

        // 数据接收回调方法
        private Sprite LoadSprite(string spriteName)
        {
            if (string.IsNullOrEmpty(spriteName))
                return null;

            // 尝试从Resources/Items路径加载
            Sprite sprite = Resources.Load<Sprite>($"Items/{spriteName}");
            if (sprite == null)
            {
                Debug.LogError($"无法加载图片: {spriteName}");
            }
            return sprite;
        }

        public void OnItemsReceived(List<ServerItemDetails> serverItems)
        {
            if (ItemDataList_SO.itemDetailsList == null)
                ItemDataList_SO.itemDetailsList = new List<ItemDetails>();
            else
                ItemDataList_SO.itemDetailsList.Clear();

            foreach (var serverItem in serverItems)
            { 
                Sprite itemSprite = LoadSprite(serverItem.ItemSpriteName);

                var localItem = new ItemDetails
                {
                    ItemID = serverItem.ItemID,
                    ItemName = serverItem.ItemName,
                    ItemPrice = serverItem.ItemPrice,
                    ItemSpriteName = serverItem.ItemSpriteName,
                    ItemSprite = itemSprite 
                };

                ItemDataList_SO.itemDetailsList.Add(localItem);
            }

        }

        public void OnInventoryReceived(List<ServerInventoryItem> serverInventory)
        {
            if (InventoryBag_SO.itemList == null)
                InventoryBag_SO.itemList = new List<InventoryItem>();
            else
                InventoryBag_SO.itemList.Clear();

            foreach (var serverItem in serverInventory)
            {
                InventoryBag_SO.itemList.Add(new InventoryItem
                {
                    SlotIndex = serverItem.SlotIndex,
                    ItemID = serverItem.ItemID,
                    ItemAmount = serverItem.ItemAmount
                });
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
        }

        public void OnStoreItemsReceived(List<ServerStoreItem> serverStoreItems)
        {
            if (stroeList_SO.Stroelist == null)
                stroeList_SO.Stroelist = new List<Store>();
            else
                stroeList_SO.Stroelist.Clear();

            foreach (var serverItem in serverStoreItems)
            {
                stroeList_SO.Stroelist.Add(new Store
                {
                    ItemID = serverItem.ItemID,
                    ItemPrice = serverItem.StorePrice
                });
            }

            EventHandler.CallUpdateStoreUI(stroeList_SO.Stroelist);
        }

        public void OnMoneyReceived(ServerPlayerMoney money)
        {
            if (MoneyDataList_SO.MoneyList == null)
                MoneyDataList_SO.MoneyList = new List<Money>();

            if (MoneyDataList_SO.MoneyList.Count > 0)
            {
                MoneyDataList_SO.MoneyList[0].money = money.MoneyAmount;
            }
            else
            {
                MoneyDataList_SO.MoneyList.Add(new Money { money = money.MoneyAmount });
            }
        }

        // 交易相关方法
        public void Buy(ItemDetails itemDetails)
        {
            StartCoroutine(ProcessBuy(itemDetails));
        }

        public void TradeItem(ItemDetails itemDetails)
        {
            StartCoroutine(ProcessSell(itemDetails));
        }

        private IEnumerator ProcessBuy(ItemDetails itemDetails)
        {
            int cost = itemDetails.ItemPrice;
            int playerMoney = MoneyDataList_SO.MoneyList[0].money;

            if (playerMoney >= cost)
            {
                yield return StartCoroutine(UpdateMoneyOnServer(playerMoney - cost));
                yield return StartCoroutine(AddItemToServer(itemDetails.ItemID));
            }
            else
            {
                Debug.LogWarning("金钱不足，无法购买");
            }
        }

        private IEnumerator ProcessSell(ItemDetails itemDetails)
        {
            int sellPrice = itemDetails.ItemPrice;
            int playerMoney = MoneyDataList_SO.MoneyList[0].money;
            var index = GetItemIndexInBag(itemDetails.ItemID);

            if (index != -1 && InventoryBag_SO.itemList[index].ItemAmount > 0)
            {
                yield return StartCoroutine(ReduceItemOnServer(itemDetails.ItemID));
                yield return StartCoroutine(UpdateMoneyOnServer(playerMoney + sellPrice));
            }
            else
            {
                Debug.LogWarning("物品不存在或数量不足，无法出售");
            }
        }

        // 网络操作方法
        private IEnumerator UpdateMoneyOnServer(int newAmount)
        {
            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
            {
                var updateData = new { amount = newAmount };
                string jsonData = JsonConvert.SerializeObject(updateData); 
                bool updateCompleted = false;
                SocketManager.Instance.SendMessage("UPDATE_MONEY", jsonData, (response) => {
                    updateCompleted = true;
                });

                yield return new WaitUntil(() => updateCompleted);

                if (MoneyDataList_SO.MoneyList.Count > 0)
                {
                    MoneyDataList_SO.MoneyList[0].money = newAmount;
                }
                else
                {
                    MoneyDataList_SO.MoneyList.Add(new Money { money = newAmount });
                }
            }
        }

        private IEnumerator AddItemToServer(int itemID)
        {
            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
            {
                var index = GetItemIndexInBag(itemID);
                int currentAmount = index != -1 ? InventoryBag_SO.itemList[index].ItemAmount + 1 : 1;
                int slotIndex = index != -1 ? index : FindEmptySlot();

                if (slotIndex != -1)
                {
                    var updateData = new
                    {
                        slotIndex = slotIndex,
                        itemId = itemID,
                        amount = currentAmount
                    };
                    string jsonData = JsonConvert.SerializeObject(updateData);

                    bool updateCompleted = false;
                    SocketManager.Instance.SendMessage("UPDATE_INVENTORY", jsonData, (response) => {
                        updateCompleted = true;
                    });

                    yield return new WaitUntil(() => updateCompleted);

                    if (index != -1)
                    {
                        InventoryBag_SO.itemList[index].ItemAmount = currentAmount;
                    }
                    else
                    {
                        InventoryBag_SO.itemList[slotIndex] = new InventoryItem
                        {
                            SlotIndex = slotIndex,
                            ItemID = itemID,
                            ItemAmount = currentAmount
                        };
                    }

                    EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
                }
            }
        }

        private IEnumerator ReduceItemOnServer(int itemID)
        {
            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
            {
                var index = GetItemIndexInBag(itemID);
                if (index != -1)
                {
                    int newAmount = InventoryBag_SO.itemList[index].ItemAmount - 1;
                    var updateData = new
                    {
                        slotIndex = index,
                        itemId = itemID,
                        amount = newAmount
                    };
                    string jsonData = JsonConvert.SerializeObject(updateData); 
                    bool updateCompleted = false;
                    SocketManager.Instance.SendMessage("UPDATE_INVENTORY", jsonData, (response) => {
                        updateCompleted = true;
                    });

                    yield return new WaitUntil(() => updateCompleted);
                    if (newAmount > 0)
                    {
                        InventoryBag_SO.itemList[index].ItemAmount = newAmount;
                    }
                    else
                    {
                        InventoryBag_SO.itemList[index] = new InventoryItem
                        {
                            SlotIndex = index,
                            ItemID = 0,
                            ItemAmount = 0
                        };
                    }

                    EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
                }
            }
        }

        private int GetItemIndexInBag(int ID)
        {
            if (InventoryBag_SO.itemList == null) return -1;

            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
            {
                if (InventoryBag_SO.itemList[i].ItemID == ID)
                    return i;
            }
            return -1;
        }

        private int FindEmptySlot()
        {
            if (InventoryBag_SO.itemList == null) return -1;

            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
            {
                if (InventoryBag_SO.itemList[i].ItemID == 0)
                    return i;
            }
            return -1;
        }

        public ItemDetails GetItemDetails(int ID)
        {
            if (ItemDataList_SO == null || ItemDataList_SO.itemDetailsList == null) return null;
            return ItemDataList_SO.itemDetailsList.Find(i => i.ItemID == ID);
        }


        public void AddItem(int itemID)
        {
            if (SocketManager.Instance != null && SocketManager.Instance.IsConnected)
            {
                var index = GetItemIndexInBag(itemID);
                int currentAmount = index != -1 ? InventoryBag_SO.itemList[index].ItemAmount + 1 : 1;
                int slotIndex = index != -1 ? index : FindEmptySlot();

                if (slotIndex != -1)
                {
                    var updateData = new { slotIndex = slotIndex, itemId = itemID, amount = currentAmount };
                    string jsonData = JsonUtility.ToJson(updateData);
                    SocketManager.Instance.SendMessage("UPDATE_INVENTORY", jsonData);
                }
            }
            else
            {
                var index = GetItemIndexInBag(itemID);
                if (index != -1)
                {
                    InventoryBag_SO.itemList[index].ItemAmount += 1;
                }
                else
                {
                    int emptySlot = FindEmptySlot();
                    if (emptySlot != -1)
                    {
                        InventoryBag_SO.itemList[emptySlot] = new InventoryItem
                        {
                            SlotIndex = emptySlot,
                            ItemID = itemID,
                            ItemAmount = 1
                        };
                    }
                }
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
            }
        }

        public void SwapItem(int fromIndex, int targetIndex)
        {
            if (InventoryBag_SO.itemList == null) return;

            InventoryItem currentItem = InventoryBag_SO.itemList[fromIndex];
            InventoryItem targetItem = InventoryBag_SO.itemList[targetIndex];

            if (targetItem.ItemID != 0)
            {
                InventoryBag_SO.itemList[fromIndex] = targetItem;
                InventoryBag_SO.itemList[targetIndex] = currentItem;
            }
            else
            {
                InventoryBag_SO.itemList[targetIndex] = currentItem;
                InventoryBag_SO.itemList[fromIndex] = new InventoryItem();
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
        }

        public void Reduce(int ID)
        {
            var index = GetItemIndexInBag(ID);
            if (index != -1)
            {
                var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
                if (amount > 0)
                {
                    InventoryBag_SO.itemList[index].ItemAmount = amount;
                }
                else
                {
                    InventoryBag_SO.itemList[index] = new InventoryItem
                    {
                        SlotIndex = index,
                        ItemID = 0,
                        ItemAmount = 0
                    };
                }
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
            }
        }

        // 本地数据操作方法（当服务器不可用时使用）
        private bool backpackvacancy()
        {
            for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
            {
                if (InventoryBag_SO.itemList[i].ItemID == 0)
                    return true;
            }
            return false;
        }

        private void AddItemAtIndex(int ID, int index, int amount)
        {
            if (index == -1 && backpackvacancy())
            {
                if (amount > 99)
                {
                    var item1 = new InventoryItem { ItemID = ID, ItemAmount = amount - 99 };
                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
                    {
                        if (InventoryBag_SO.itemList[i].ItemID == 0)
                        {
                            InventoryBag_SO.itemList[i] = item1;
                            break;
                        }
                    }
                    var item = new InventoryItem { ItemID = ID, ItemAmount = 99 };
                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
                    {
                        if (InventoryBag_SO.itemList[i].ItemID == 0)
                        {
                            InventoryBag_SO.itemList[i] = item;
                            break;
                        }
                    }
                }
                else
                {
                    var item = new InventoryItem { ItemID = ID, ItemAmount = amount };
                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
                    {
                        if (InventoryBag_SO.itemList[i].ItemID == 0)
                        {
                            InventoryBag_SO.itemList[i] = item;
                            break;
                        }
                    }
                }
            }
            else
            {
                int currentAmount = InventoryBag_SO.itemList[index].ItemAmount + amount;
                if (currentAmount > 99)
                {
                    var item1 = new InventoryItem { ItemID = ID, ItemAmount = currentAmount - 99 };
                    for (int i = 0; i < InventoryBag_SO.itemList.Count; i++)
                    {
                        if (InventoryBag_SO.itemList[i].ItemID == 0)
                        {
                            InventoryBag_SO.itemList[i] = item1;
                            break;
                        }
                    }
                    var item2 = new InventoryItem { ItemID = ID, ItemAmount = 99 };
                    InventoryBag_SO.itemList[index] = item2;
                }
                else
                {
                    var item = new InventoryItem { ItemID = ID, ItemAmount = currentAmount };
                    InventoryBag_SO.itemList[index] = item;
                }
            }
        }

        public void SellItem(int itemID)
        {
            var index = GetItemIndexInBag(itemID);

            if (InventoryBag_SO.itemList[index].ItemAmount >= 1)
            {
                var amount = InventoryBag_SO.itemList[index].ItemAmount - 1;
                var item = new InventoryItem { ItemID = itemID, ItemAmount = amount };
                InventoryBag_SO.itemList[index] = item;
                if (amount == 0)
                {
                    var item1 = new InventoryItem { ItemID = 0, ItemAmount = 0 };
                    InventoryBag_SO.itemList[index] = item1;
                }
            }
            else if (InventoryBag_SO.itemList[index].ItemAmount == 0)
            {
                var item = new InventoryItem();
                InventoryBag_SO.itemList[index] = item;
            }
        }
        public void LocalTradeItem(ItemDetails itemDetails)
        {
            int cost = itemDetails.ItemPrice;
            int index = GetItemIndexInBag(itemDetails.ItemID);

            // 卖
            if (InventoryBag_SO.itemList[index].ItemAmount >= 1)
            {
                SellItem(itemDetails.ItemID);
                // 金额
                int moneyItem = MoneyDataList_SO.MoneyList[0].money;
                var item = new Money { money = moneyItem + cost };
                MoneyDataList_SO.MoneyList[0] = item;
            }
            else
            {
                return;
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
        }

        public void LocalBuy(ItemDetails itemDetails)
        {
            int cost = itemDetails.ItemPrice;
            int index = GetItemIndexInBag(itemDetails.ItemID);
            if (MoneyDataList_SO.MoneyList[0].money >= cost)
            {
                AddItemAtIndex(itemDetails.ItemID, index, 1);
                int moneyItem = MoneyDataList_SO.MoneyList[0].money;
                var item = new Money { money = moneyItem - cost };
                MoneyDataList_SO.MoneyList[0] = item;
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryBag_SO.itemList);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("item"))
                    {
                        Item itemComponent = hit.collider.gameObject.GetComponent<Item>();

                        if (itemComponent != null)
                        {
                            InventoryManager.Instance.AddItem(itemComponent.ItemID);
                            print(itemComponent.ItemID);
                            Destroy(itemComponent.gameObject);
                        }
                    }
                }
            }
        }
    }
}