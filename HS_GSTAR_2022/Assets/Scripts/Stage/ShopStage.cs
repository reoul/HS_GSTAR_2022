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
        Logger.Log("상점 스테이지 입장 로직 시작");
        
        int itemIndex1, itemIndex2, itemIndex3;
        itemIndex1 = Random.Range(0, ItemInfoArray.Length);
        do
        {
            itemIndex2 = Random.Range(0, ItemInfoArray.Length);
            itemIndex3 = Random.Range(0, ItemInfoArray.Length);
        } while (itemIndex1 == itemIndex3 || itemIndex1 == itemIndex2 || itemIndex2 == itemIndex3);

        ItemCards[0].SetInfo(ItemInfoArray[itemIndex1]);
        Logger.Log($"첫 번째 아이템({itemIndex1}) {ItemInfoArray[itemIndex1]} 으로 설정됨");
        ItemCards[1].SetInfo(ItemInfoArray[itemIndex2]);
        Logger.Log($"두 번째 아이템({itemIndex2}) {ItemInfoArray[itemIndex2]} 으로 설정됨");
        ItemCards[2].SetInfo(ItemInfoArray[itemIndex3]);
        Logger.Log($"세 번째 아이템({itemIndex3}) {ItemInfoArray[itemIndex3]} 으로 설정됨");
        
        Logger.Log("상점 스테이지 입장 로직 종료");
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
        Logger.Log("상점 스테이지 퇴장 로직 시작");
        Logger.Log("상점 스테이지 퇴장 로직 종료");
    }
}
