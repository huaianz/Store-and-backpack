using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace T.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    //InventoryManager.Instance.AddItem(itemDetails);
                    if (hit.collider.CompareTag("item"))
                    {
                        //ÃÌº”ŒÔ∆∑
                        //Item item = hit.collider.gameObject.GetComponent<Item>();
                        //InventoryManager.Instance.AddItem(item.ItemID);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}
