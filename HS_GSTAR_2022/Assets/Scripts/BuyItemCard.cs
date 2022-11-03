using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuyItemCard : MonoBehaviour
{
    public void BuyCard()
    {
        BackPack.Instance.AddItem(this.gameObject);
    }
}
