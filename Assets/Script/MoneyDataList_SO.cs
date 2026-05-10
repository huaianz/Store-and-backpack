using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Money_SO", menuName = "MoneyListSO")]
[System.Serializable]
public class MoneyDataList_SO : ScriptableObject
{
    public List<Money> MoneyList;
}
