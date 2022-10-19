using System;

/// <summary> 주사위 눈금 1 ~ 3, 4 ~ 6 발동 카드 </summary>
public abstract class CardBase33 : Card
{
    protected abstract string Name { get; }
    public sealed override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 설명 </summary>
    protected abstract string Description123 { get; }

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 설명 </summary>
    protected abstract string Description456 { get; }

    public sealed override string GetDescription() => $"1~3: {Description123}\n" +
                                                     $"4~6: {Description456}\n";

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected abstract string Use123();

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected abstract string Use456();

    public sealed override void Use(Dice dice)
    {
        string description = "empty";
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
            case EDiceNumber.Three:
                description = Use123();
                break;
            case EDiceNumber.Four:
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                description = Use456();
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }
        Logger.Log($"{Name} : {dice} : {description}");
        CardManager.Instance.RemoveCard(this);
        Destroy();  // 카드 삭제
    }
}
