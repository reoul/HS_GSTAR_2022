using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 주사위 눈금 1 ~ 6 발동 카드 </summary>
public sealed class CardBase6 : Card
{
    public string Name { get; set; }
    public override string GetName() => Name;

    /// <summary> 모든 주사위 눈금 발동 효과 설명 </summary>
    public string Description { get; set; }

    public override string GetDescription() => Description;

    public EventCardEffectType EffectType { get; set; }

    public override void Use(Dice dice)
    {
        ApplyEffect(EffectType, (int) dice.Number);
        Logger.Log($"{Name} : {dice} : {Description}");
        StartDestroyAnimation(); // 카드 삭제
    }
}
