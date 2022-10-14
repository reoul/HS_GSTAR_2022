using System.Collections.Generic;
using System;

public abstract class CardBase222_JGS : Card
{
    protected abstract string Name { get; }
    public sealed override string GetName() => Name;

    /// <summary> 카드의 정보를 담은 구조체 (설명, 매개변수) </summary>
    protected struct Info
    {
        public string description;
        public List<int> values;
    }

    /// <summary> 주사위 눈금 1 ~ 2번에 담을 정보 </summary>
    protected abstract Info Information12 { get; }

    /// <summary> 주사위 눈금 3 ~ 4번에 담을 정보 </summary>
    protected abstract Info Information34 { get; }

    /// <summary> 주사위 눈금 5 ~ 6번에 담을 정보 </summary>
    protected abstract Info Information56 { get; }

    public sealed override string GetDescription() => $"1~2: {Information12.description}\n" +
                                                     $"3~4: {Information34.description}\n" +
                                                     $"5~6 : {Information56.description}\n";

    /// <summary> 주사위 눈금 1 ~ 2번 발동 효과 </summary>
    protected abstract void Use12();

    /// <summary> 주사위 눈금 3 ~ 4번 발동 효과 </summary>
    protected abstract void Use34();

    /// <summary> 주사위 눈금 5 ~ 6번 발동 효과 </summary>
    protected abstract void Use56();

    public sealed override void Use(Dice dice)
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
        Destroy();  // 주사위 삭제
    }
}
