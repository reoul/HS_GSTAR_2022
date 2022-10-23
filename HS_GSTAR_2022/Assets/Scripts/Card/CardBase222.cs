using System;

/// <summary> 주사위 눈금 1 ~ 2, 3 ~ 4, 5 ~ 6 발동 카드 </summary>
public abstract class CardBase222 : Card
{
    protected abstract string Name { get; }
    public sealed override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 2번에 발동 효과 설명 </summary>
    protected abstract string Description12 { get; }

    /// <summary> 주사위 눈금 3 ~ 4번에 발동 효과 설명 </summary>
    protected abstract string Description34 { get; }

    /// <summary> 주사위 눈금 5 ~ 6번에 발동 효과 설명 </summary>
    protected abstract string Description56 { get; }

    public sealed override string GetDescription() => $"1~2: {Description12}\n" +
                                                      $"3~4: {Description34}\n" +
                                                      $"5~6 : {Description56}\n";

    /// <summary> 주사위 눈금 1 ~ 2번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected abstract string Use12();

    /// <summary> 주사위 눈금 3 ~ 4번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected abstract string Use34();

    /// <summary> 주사위 눈금 5 ~ 6번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected abstract string Use56();

    public sealed override void Use(Dice dice)
    {
        string description;
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
                description = Use12();
                break;
            case EDiceNumber.Three:
            case EDiceNumber.Four:
                description = Use34();
                break;
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                description = Use56();
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Logger.Log($"{Name} : {dice} : {description}");
        CardManager.Instance.RemoveCard(this);
        StartDestroyAnimation();  // 카드 삭제
    }
}
