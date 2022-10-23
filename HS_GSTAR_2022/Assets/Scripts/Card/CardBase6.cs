using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 주사위 눈금 1 ~ 6 발동 카드 </summary>
public abstract class CardBase6 : Card
{
    protected abstract string Name { get; }
    public sealed override string GetName() => Name;
    
    /// <summary> 모든 주사위 눈금 발동 효과 설명 </summary>
    protected abstract string Description { get; }

    public sealed override string GetDescription() => Description;

    /// <summary> 모든 주사위 눈금 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected abstract string UseCard(Dice dice);
    
    public sealed override void Use(Dice dice)
    {
        UseCard(dice);
        Logger.Log($"{Name} : {dice} : {Description}");
        CardManager.Instance.RemoveCard(this);
        StartDestroyAnimation();  // 카드 삭제
    }
}
