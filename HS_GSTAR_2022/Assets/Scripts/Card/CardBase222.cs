using System;
using UnityEngine.Assertions;

public abstract class CardBase222 : Card
{
    protected abstract string Name { get; }
    public override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 2번에 발동 효과 설명 </summary>
    protected abstract string Description12 { get; }

    /// <summary> 주사위 눈금 3 ~ 4번에 발동 효과 설명 </summary>
    protected abstract string Description34 { get; }

    /// <summary> 주사위 눈금 5 ~ 6번에 발동 효과 설명 </summary>
    protected abstract string Description56 { get; }

    public override string GetDescription() => $"{Description12}\n{Description34}\n{Description56}\n";

    /// <summary> 주사위 눈금 1 ~ 2번 발동 효과 </summary>
    protected abstract void Use12();

    /// <summary> 주사위 눈금 3 ~ 4번 발동 효과 </summary>
    protected abstract void Use34();

    /// <summary> 주사위 눈금 5 ~ 6번 발동 효과 </summary>
    protected abstract void Use56();

    public override void Use(Dice dice)
    {
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
                Use12();
                break;
            case EDiceNumber.Three:
            case EDiceNumber.Four:
                Use34();
                break;
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                Use56();
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
