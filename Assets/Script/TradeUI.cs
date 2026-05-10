//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//namespace T.Inventory
//{
//    public class TradeUI : MonoBehaviour
//    {
//        public Image itemIcon;
//        public Button Button;

//        public ItemDetails item;
//        private bool isSellTrade;

//        private void Awake()
//        {
//            Button.onClick.AddListener(TradeItem);

//        }
//        public void SetupTradeUI(ItemDetails item, bool isSell)
//        {
//            this.item = item;
//            itemIcon.sprite = item.ItemSprite;
//            isSellTrade = isSell;
//        }

//        public void CancelTrade()
//        {
//            this.gameObject.SetActive(false);

//        }

//        private void TradeItem()
//        {
//            InventoryManager.Instance.TradeItem(item);      
//        }
//    }
//}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace T.Inventory
{
    public class TradeUI : MonoBehaviour
    {
        public Image itemIcon;
        public Button Button;

        public ItemDetails item;
        private bool isSellTrade;

        private void Awake()
        {
            Button.onClick.AddListener(TradeItem);
        }

        public void SetupTradeUI(ItemDetails item, bool isSell)
        {
            this.item = item;
            itemIcon.sprite = item.ItemSprite;
            isSellTrade = isSell;
        }

        public void CancelTrade()
        {
            this.gameObject.SetActive(false);
        }

        private void TradeItem()
        {
            if (item == null) return;

            if (isSellTrade)
            {
                // 出售物品
                if (InventoryManager.Instance != null)
                {
                    InventoryManager.Instance.TradeItem(item);
                }
            }
            else
            {
                // 购买物品
                if (InventoryManager.Instance != null)
                {
                    InventoryManager.Instance.Buy(item);
                }
            }

            this.gameObject.SetActive(false);
        }
    }
}