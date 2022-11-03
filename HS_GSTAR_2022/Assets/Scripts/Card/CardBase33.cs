using System;
using UnityEngine;

/// <summary> 주사위 눈금 1 ~ 3, 4 ~ 6 발동 카드 </summary>
public sealed class CardBase33 : Card
{
    public string Name { get; set; }
    public override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 설명 </summary>
    public string Description123 { get; set; }

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 설명 </summary>
    public string Description456 { get; set; }

    public override string GetDescription() => $"1~3: {Description123}\n" +
                                               $"4~6: {Description456}\n";
    
    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList123;
    
    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList456;
    
    public override void Use(Dice dice)
    {
        string description;
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
            case EDiceNumber.Three:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList123)
                {
                    ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                }
                description = Description123;
                break;
            case EDiceNumber.Four:
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList456)
                {
                    ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                }
                description = Description456;
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Logger.Log($"{Name} : {dice} : {description}");
        StartDestroyAnimation(); // 카드 삭제
    }
}
