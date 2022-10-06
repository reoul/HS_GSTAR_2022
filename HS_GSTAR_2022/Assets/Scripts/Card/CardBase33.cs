using System;

public abstract class CardBase33 : Card
{
    protected abstract string Name { get; }
    public override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 설명 </summary>
    protected abstract string Description123 { get; }

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 설명 </summary>
    protected abstract string Description456 { get; }

    public override string GetDescription() => $"{Description123}\n{Description456}\n";

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 </summary>
    protected abstract void Use123();

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 </summary>
    protected abstract void Use456();

    public override void Use(Dice dice)
    {
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
            case EDiceNumber.Three:
                Use123();
                break;
            case EDiceNumber.Four:
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                Use456();
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
