using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : Singleton<BackPack>
{
    public List<GameObject> Items;

    [SerializeField]
    private GameObject inventory, ItemParent;

    private void Start()
    {
        Items = new List<GameObject>();
    }

    public void AddItem(GameObject obj)
    {
        Items.Add(obj);
        obj.transform.SetParent(ItemParent.transform);
        obj.transform.localScale = new Vector3(1, 1, 1);
    }


    public void OpenInventory()
    {
        inventory.SetActive(!inventory.activeSelf);

        foreach(GameObject obj in Items)
        {
            obj.transform.localPosition = Vector3.zero;
        }
    }
}
