//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;
//using UnityEngine.UI;
//namespace T.Inventory
//{

//    public class InventoryUI : MonoBehaviour
//    {
//        [Header("拖拽图片")]
//        public Image dragItem;

//        [Header("玩家背包UI")]
//        [SerializeField] private GameObject BagUI;

//        [Header("商店UI")]
//        [SerializeField] private GameObject StoreUI;

//        [SerializeField] private Slot_UI[] playerSlots;
//        [SerializeField] private Slot_UI[] StoreSlots;

//        [Header("金币UI")]
//        [SerializeField] private Text Text;

//        [Header("交易UI")]
//        [SerializeField] private TradeUI TradeUI;

//        public Button BagButton;
//        public Button StoreButton;

//        private bool bagOpened;
//        private bool storeOpened;

//        private void OnEnable()
//        {
//            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
//            EventHandler.UpdateStoreUI += OnUpadtStoreUI;
//            EventHandler.TradeUI += OnTradeUI;
//            //更新钱的UI

//        }


//        private void OnDisable()
//        {
//            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
//            EventHandler.UpdateStoreUI -= OnUpadtStoreUI;
//            EventHandler.TradeUI -= OnTradeUI;
//        }

//        private void OnTradeUI(List<Money> list,int cost)
//        {


//        }
//        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
//        {
//            switch (location)
//            {
//                case InventoryLocation.Bag:
//                    for (int i = 0; i < playerSlots.Length; i++)
//                    {
//                        if (list[i].ItemAmount > 0)
//                        {
//                            var item = InventoryManager.Instance.GetItemDetails(list[i].ItemID);
//                            playerSlots[i].UpdateSlot(item, list[i].ItemAmount);
//                        }
//                        else
//                        {
//                            playerSlots[i].UpdateEmptySlot();
//                        }
//                    }
//                    break;
//            }
//        }

//        public void OnUpadtStoreUI(List<Store> list)
//        {
//            for(int i = 0;i < StoreSlots.Length;i++)
//            {
//                var item = InventoryManager.Instance.GetItemDetails(list[i].ItemID);
//                StoreSlots[i].StoreSlot(item, list[i].ItemPrice);
//            }
//        }



//        private void Start()
//        {
//            for (int i = 0; i < playerSlots.Length; i++)
//            {
//                playerSlots[i].slotIndex = i;
//            }
//            for (int i = 0; i < StoreSlots.Length; i++)
//            {
//                StoreSlots[i].slotIndex = i;
//            }
//            bagOpened = BagUI.activeInHierarchy;
//            storeOpened = StoreUI.activeInHierarchy;
//            Text.text = InventoryManager.Instance.MoneyDataList_SO.MoneyList[0].money.ToString();
//        }

//        private void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.B))
//            {
//                OpenBagUI();
//            }
//            if (Input.GetKeyDown(KeyCode.A))
//            {
//                OpenStoreUI();
//            }
//            //填写背包和商店的打开关闭
//            //BagButton.onClick.AddListener(OpenBagUI);
//            //StoreButton.onClick.AddListener(OpenStoreUI);
//            Text.text = InventoryManager.Instance.MoneyDataList_SO.MoneyList[0].money.ToString();
//        }

//        private void OpenBagUI()
//        {
//            bagOpened = !bagOpened;
//            BagUI.SetActive(bagOpened);
//        }
//        private void OpenStoreUI()
//        {
//            storeOpened = !storeOpened;
//            StoreUI.SetActive(storeOpened);
//        }



//    }
//}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
namespace T.Inventory
{

    public class InventoryUI : MonoBehaviour
    {
        [Header("拖拽物品")]
        public Image dragItem;

        [Header("背包相关UI")]
        [SerializeField] private GameObject BagUI;

        [Header("商店UI")]
        [SerializeField] private GameObject StoreUI;

        [SerializeField] private Slot_UI[] playerSlots;
        [SerializeField] private Slot_UI[] StoreSlots;

        [Header("金钱UI")]
        [SerializeField] private Text Text;

        [Header("交易UI")]
        [SerializeField] private TradeUI TradeUI;

        public Button BagButton;
        public Button StoreButton;

        private bool bagOpened;
        private bool storeOpened;

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
            EventHandler.UpdateStoreUI += OnUpadtStoreUI;
            EventHandler.TradeUI += OnTradeUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
            EventHandler.UpdateStoreUI -= OnUpadtStoreUI;
            EventHandler.TradeUI -= OnTradeUI;
        }

        private void OnTradeUI(List<Money> list, int cost)
        {
            // 交易UI逻辑
        }

        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Bag:
                    // 修复：确保不超出数组边界
                    int slotCount = Mathf.Min(playerSlots.Length, list.Count);

                    for (int i = 0; i < slotCount; i++)
                    {
                        if (list[i].ItemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].ItemID);
                            playerSlots[i].UpdateSlot(item, list[i].ItemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }

                    // 如果背包槽位多于数据项，清空多余的槽位
                    for (int i = slotCount; i < playerSlots.Length; i++)
                    {
                        playerSlots[i].UpdateEmptySlot();
                    }
                    break;
            }
        }

        public void OnUpadtStoreUI(List<Store> list)
        {
            // 修复：确保不超出数组边界
            int slotCount = Mathf.Min(StoreSlots.Length, list.Count);

            for (int i = 0; i < slotCount; i++)
            {
                var item = InventoryManager.Instance.GetItemDetails(list[i].ItemID);
                StoreSlots[i].StoreSlot(item, list[i].ItemPrice);
            }

            // 如果商店槽位多于数据项，清空多余的槽位
            for (int i = slotCount; i < StoreSlots.Length; i++)
            {
                StoreSlots[i].UpdateEmptySlot();
            }
        }

        private void Start()
        {
            // 确保数组初始化
            if (playerSlots == null) playerSlots = new Slot_UI[0];
            if (StoreSlots == null) StoreSlots = new Slot_UI[0];

            for (int i = 0; i < playerSlots.Length; i++)
            {
                playerSlots[i].slotIndex = i;
            }
            for (int i = 0; i < StoreSlots.Length; i++)
            {
                StoreSlots[i].slotIndex = i;
            }

            bagOpened = BagUI.activeInHierarchy;
            storeOpened = StoreUI.activeInHierarchy;

            // 安全地获取金钱显示
            if (InventoryManager.Instance != null &&
                InventoryManager.Instance.MoneyDataList_SO != null &&
                InventoryManager.Instance.MoneyDataList_SO.MoneyList != null &&
                InventoryManager.Instance.MoneyDataList_SO.MoneyList.Count > 0)
            {
                Text.text = InventoryManager.Instance.MoneyDataList_SO.MoneyList[0].money.ToString();
            }
            else
            {
                Text.text = "1000"; // 默认值
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                OpenStoreUI();
            }

            // 安全地更新金钱显示
            if (InventoryManager.Instance != null &&
                InventoryManager.Instance.MoneyDataList_SO != null &&
                InventoryManager.Instance.MoneyDataList_SO.MoneyList != null &&
                InventoryManager.Instance.MoneyDataList_SO.MoneyList.Count > 0)
            {
                Text.text = InventoryManager.Instance.MoneyDataList_SO.MoneyList[0].money.ToString();
            }
        }

        private void OpenBagUI()
        {
            bagOpened = !bagOpened;
            BagUI.SetActive(bagOpened);
        }
        private void OpenStoreUI()
        {
            storeOpened = !storeOpened;
            StoreUI.SetActive(storeOpened);
        }
    }
}