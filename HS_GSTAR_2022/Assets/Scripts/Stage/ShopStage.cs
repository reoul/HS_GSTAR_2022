using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShopStage : Stage
{
    public ItemInfo[] ItemInfoArray { get; set; }
    public ItemCard[] ItemCards;

    public override void StageEnter()
    {
        int itemIndex1, itemIndex2, itemIndex3;
        itemIndex1 = Random.Range(0, ItemInfoArray.Length);
        do
        {
            itemIndex2 = Random.Range(0, ItemInfoArray.Length);
            itemIndex3 = Random.Range(0, ItemInfoArray.Length);
        } while (itemIndex1 == itemIndex3 || itemIndex1 == itemIndex2 || itemIndex2 == itemIndex3);

        ItemCards[0].SetInfo(ItemInfoArray[itemIndex1]);
        ItemCards[1].SetInfo(ItemInfoArray[itemIndex2]);
        ItemCards[2].SetInfo(ItemInfoArray[itemIndex3]);
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
    }
}
