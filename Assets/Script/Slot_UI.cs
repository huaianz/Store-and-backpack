//using System;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using System.Diagnostics;

//namespace T.Inventory
//{
//    public class Slot_UI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
//    {
//        [Header("组件获取")]
//        [SerializeField] private Image slotImage;
//        [SerializeField] private Text amountText;
//        [SerializeField] private Button Button;

//        [Header("格子类型")]
//        public SlotType slotType;
//        public bool isSelected;
//        public int slotIndex;

//        public ItemDetails itemDetails;
//        public int itemAmount;
//        public int itemPrice;

//        public InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

//        private void Start()
//        {
//            if (itemDetails == null)
//                UpdateEmptySlot();
//        }

//        public void Awake()
//        {

//        }

//        //格子的数据和数量
//        public void UpdateSlot(ItemDetails item, int amount)
//        {
//            itemDetails = item;
//            slotImage.sprite = item.ItemSprite;
//            itemAmount = amount;
//            amountText.text = amount.ToString();
//            slotImage.enabled = true;
//            Button.interactable = true;
//        }

//        public void StoreSlot(ItemDetails item,int price)
//        {
//            itemDetails = item;
//            slotImage.sprite = item.ItemSprite;
//            itemPrice = price;
//            amountText.text = price.ToString();
//            slotImage.enabled = true;
//            Button.interactable = true;
//        }

//        //没有数据时，所有都清空
//        //public void UpdateEmptySlot()
//        //{
//        //    itemDetails = null;
//        //    slotImage.enabled = false;                            
//        //    amountText.text = string.Empty;
//        //    Button.interactable = false;
//        //}
//        // 在 Slot_UI.cs 的 UpdateEmptySlot 方法中确保完全清空
//        public void UpdateEmptySlot()
//        {
//            itemDetails = null;
//            itemAmount = 0;
//            itemPrice = 0;
//            slotImage.enabled = false;
//            amountText.text = string.Empty;
//            Button.interactable = false;

//            // 可选：设置默认透明图片
//            // slotImage.sprite = null;
//        }

//        public void OnPointerClick(PointerEventData eventData)
//        {
//            if(itemDetails == null)return;
//            //isSelected=!isSelected;
//            //if (slotType == SlotType.Store)
//            //{
//            //    InventoryManager.Instance.Sell(InventoryManager.Instance.MoneyDataList_SO.MoneyList[0].money, itemPrice);
//            //    Console.WriteLine("结果是：");
//            //}
//        }


//        public void OnBeginDrag(PointerEventData eventData)
//        {
//            if(itemAmount!=0)
//            {
//                inventoryUI.dragItem.enabled=true;
//                inventoryUI.dragItem.sprite=slotImage.sprite;
//                inventoryUI.dragItem.SetNativeSize();

//            }
//        }

//        public void OnDrag(PointerEventData eventData)
//        {
//            inventoryUI.dragItem.transform.position=Input.mousePosition;
//        }

//        public void OnEndDrag(PointerEventData eventData)
//        {
//            inventoryUI.dragItem.enabled = false;

//            if(eventData.pointerCurrentRaycast.gameObject!=null)
//            {
//                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot_UI>() == null)
//                    return;

//                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot_UI>();
//                int targetIndex=targetSlot.slotIndex;

//                if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
//                {

//                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
//                }
//                else if (slotType == SlotType.Store && targetSlot.slotType == SlotType.Bag)
//                {
//                    InventoryManager.Instance.Buy(itemDetails);
//                }//买
//                else if(slotType == SlotType.Bag && targetSlot.slotType == SlotType.Store)
//                {
//                    InventoryManager.Instance.TradeItem(itemDetails);
//                }//卖

//            }
//            else
//            {
//                var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
//                EventHandler.CallInstantiateItemInScene(itemDetails.ItemID, pos);
//                //减去背包里的数量
//                InventoryManager.Instance.Reduce(itemDetails.ItemID);
//            }

//        }
//    }
//}


using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Diagnostics;

namespace T.Inventory
{
    public class Slot_UI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("组件获取")]
        [SerializeField] private Image slotImage;
        [SerializeField] private Text amountText;
        [SerializeField] private Button Button;

        [Header("格子类型")]
        public SlotType slotType;
        public bool isSelected;
        public int slotIndex;

        public ItemDetails itemDetails;
        public int itemAmount;
        public int itemPrice;

        public InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private void Start()
        {
            if (itemDetails == null)
                UpdateEmptySlot();
        }

        public void Awake()
        {

        }

        // 更新物品格子
        public void UpdateSlot(ItemDetails item, int amount)
        {
            itemDetails = item;
            slotImage.sprite = item.ItemSprite;
            itemAmount = amount;
            amountText.text = amount.ToString();
            slotImage.enabled = true;
            Button.interactable = true;
        }

        public void StoreSlot(ItemDetails item, int price)
        {
            itemDetails = item;
            slotImage.sprite = item.ItemSprite;
            itemPrice = price;
            amountText.text = price.ToString();
            slotImage.enabled = true;
            Button.interactable = true;
        }

        // 清空格子
        public void UpdateEmptySlot()
        {
            itemDetails = null;
            itemAmount = 0;
            itemPrice = 0;
            slotImage.enabled = false;
            amountText.text = string.Empty;
            Button.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (itemDetails == null) return;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (itemAmount != 0)
            {
                inventoryUI.dragItem.enabled = true;
                inventoryUI.dragItem.sprite = slotImage.sprite;
                inventoryUI.dragItem.SetNativeSize();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            inventoryUI.dragItem.enabled = false;

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot_UI>() == null)
                    return;

                var targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<Slot_UI>();
                int targetIndex = targetSlot.slotIndex;

                if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Bag)
                {
                    InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
                }
                else if (slotType == SlotType.Store && targetSlot.slotType == SlotType.Bag)
                {
                    // 购买物品
                    if (InventoryManager.Instance != null && itemDetails != null)
                    {
                        InventoryManager.Instance.Buy(itemDetails);
                    }
                }
                else if (slotType == SlotType.Bag && targetSlot.slotType == SlotType.Store)
                {
                    // 出售物品
                    if (InventoryManager.Instance != null && itemDetails != null)
                    {
                        InventoryManager.Instance.TradeItem(itemDetails);
                    }
                }
            }
            else
            {
                // 丢弃物品到场景
                if (itemDetails != null)
                {
                    var pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                    EventHandler.CallInstantiateItemInScene(itemDetails.ItemID, pos);

                    // 减少背包里的数量
                    if (InventoryManager.Instance != null)
                    {
                        InventoryManager.Instance.Reduce(itemDetails.ItemID);
                    }
                }
            }
        }
    }
}