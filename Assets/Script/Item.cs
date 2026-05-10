//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using Unity.Burst.CompilerServices;
//using UnityEngine;
//namespace T.Inventory
//{
//    public class Item : MonoBehaviour
//    {


//        public int ItemID;
//        private BoxCollider2D coll;
//        private SpriteRenderer spriteRenderer;
//        public ItemDetails itemDetails;
//        private object console;
//        bool item = false;

//        private void Awake()
//        {
//            spriteRenderer = GetComponent<SpriteRenderer>();
//            coll = GetComponent<BoxCollider2D>();

//        }

//        private void Update()
//        {

//        }
//        private void Start()
//        {
//            if (ItemID != 0)
//            {
//                Init(ItemID);
//            }
//        }

//        private void Init(int ID)
//        {
//            ItemID = ID;
//            itemDetails = InventoryManager.Instance.GetItemDetails(ItemID);
//            if (itemDetails != null)
//            {
//                spriteRenderer.sprite = itemDetails.ItemSprite;
//            }
//        }
//    }
//}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T.Inventory
{
    public class Item : MonoBehaviour
    {
        public int ItemID;
        private BoxCollider2D coll;
        private SpriteRenderer spriteRenderer;
        public ItemDetails itemDetails;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            coll = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (ItemID != 0)
            {
                Init(ItemID);
            }
        }

        private void Init(int ID)
        {
            ItemID = ID;
            itemDetails = InventoryManager.Instance.GetItemDetails(ItemID);
            if (itemDetails != null)
            {
                spriteRenderer.sprite = itemDetails.ItemSprite;
            }
        }
    }
}