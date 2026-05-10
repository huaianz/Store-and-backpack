using System.Collections;
using System.Collections.Generic;
using T.Inventory;
using Unity.VisualScripting;
using UnityEngine;

public class itemManager : MonoBehaviour
{
    public Item itemPrefab;
    public Transform itemParent;

    private Dictionary<string,List<SceneItem>> sceneItemDict = new Dictionary<string,List<SceneItem>>();
    private void OnEnable()
    {
        EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
    }

    private void OnDisable()
    {
        EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;

    }

    private void OnInstantiateItemInScene(int ID, Vector3 pos)
    {
        var item = Instantiate(itemPrefab, pos, Quaternion.identity, itemParent);
        item.ItemID = ID;
    }

    private void Start()
    {
          
    }

}
