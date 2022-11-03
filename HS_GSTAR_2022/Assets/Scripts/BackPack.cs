using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPack : Singleton<BackPack>
{
    public List<GameObject> Items;

    [SerializeField]
    private GameObject inventory, ItemParent;

    [SerializeField]
    private GameObject cardPref;


    private void Start()
    {
        Items = new List<GameObject>();
    }

    public void AddItem(GameObject obj)
    {

        GameObject newObj = GameObject.Instantiate(cardPref, ItemParent.transform);
        newObj.GetComponent<ItemCard>().SetCard(obj.GetComponent<ItemCard>().GetCardName(), obj.GetComponent<ItemCard>().GetContext(), obj.GetComponent<ItemCard>().GetRank());

        Items.Add(newObj);
        newObj.transform.SetParent(ItemParent.transform);
        newObj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
    }


    public void OpenInventory()
    {
        inventory.SetActive(!inventory.activeSelf);
    }
}
