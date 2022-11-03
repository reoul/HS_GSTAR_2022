using System;

/// <summary> 주사위 눈금 1 ~ 2, 3 ~ 4, 5 ~ 6 발동 카드 </summary>
public sealed class CardBase222 : Card
{
    public string Name { get; set; }
    public override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 2번에 발동 효과 설명 </summary>
    public string Description12 { get; set; }

    /// <summary> 주사위 눈금 3 ~ 4번에 발동 효과 설명 </summary>
    public string Description34 { get; set; }

    /// <summary> 주사위 눈금 5 ~ 6번에 발동 효과 설명 </summary>
    public string Description56 { get; set; }

    public override string GetDescription() => $"1~2: {Description12}\n" +
                                               $"3~4: {Description34}\n" +
                                               $"5~6: {Description56}\n";


    /// <summary> 주사위 눈금 1 ~ 2번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList12;
    
    /// <summary> 주사위 눈금 3 ~ 4번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList34;
    
    /// <summary> 주사위 눈금 5 ~ 6번 발동 효과 정보 배열 </summary>
    public EventCardEffectInfo[] EffectInfoList56;

    public override void Use(Dice dice)
    {
        string description;
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList12)
                {
                    ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                }
                description = Description12;
                break;
            case EDiceNumber.Three:
            case EDiceNumber.Four:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList34)
                {
                    ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                }
                description = Description34;
                break;
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                foreach (EventCardEffectInfo effectInfo in EffectInfoList56)
                {
                    ApplyEffect(effectInfo.EventCardEffectType, (int) effectInfo.Num);
                }
                description = Description56;
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Logger.Log($"{Name} : {dice} : {description}");
        StartDestroyAnimation(); // 카드 삭제
    }
}
