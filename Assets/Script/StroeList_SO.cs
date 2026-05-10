using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "StoreList_SO", menuName = "")]
[System.Serializable]
public class StroeList_SO : ScriptableObject
{
    public List<Store> Stroelist;
}