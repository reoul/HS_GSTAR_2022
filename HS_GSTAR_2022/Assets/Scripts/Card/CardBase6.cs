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
    protected abstract void UseCard(Dice dice);
    
    public sealed override void Use(Dice dice)
    {
        UseCard(dice);
        Destroy();  // 주사위 삭제
    }
}
