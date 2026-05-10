using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;

    public static void CallUpdateInventoryUI(InventoryLocation location,List<InventoryItem> list)
    {
        UpdateInventoryUI.Invoke(location,list);
    }

    public static event Action<List<Store>> UpdateStoreUI;
    public static void CallUpdateStoreUI(List<Store> list)
    {
        UpdateStoreUI.Invoke(list);
    }



    //更新钱币
    public static event Action<List<Money>,int> TradeUI;
    public static void CallTradeUI(List<Money> list,int cost)
    {
        TradeUI?.Invoke(list,cost);
    }

    //地图上生成物品
    public static event Action<int, Vector3> InstantiateItemInScene;
    public static void CallInstantiateItemInScene(int ID, Vector3 pos)
    {
        InstantiateItemInScene?.Invoke(ID, pos);
    }


    

}
