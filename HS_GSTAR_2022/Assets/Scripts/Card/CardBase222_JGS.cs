using System.Collections.Generic;
using System;

public abstract class CardBase222_JGS : Card
{
    protected abstract string Name { get; }
    public sealed override string GetName() => Name;

    /// <summary> ī���� ������ ���� ����ü (����, �Ű�����) </summary>
    protected struct Info
    {
        public string description;
        public List<int> values;
    }

    /// <summary> �ֻ��� ���� 1 ~ 2���� ���� ���� </summary>
    protected abstract Info Information12 { get; }

    /// <summary> �ֻ��� ���� 3 ~ 4���� ���� ���� </summary>
    protected abstract Info Information34 { get; }

    /// <summary> �ֻ��� ���� 5 ~ 6���� ���� ���� </summary>
    protected abstract Info Information56 { get; }

    public sealed override string GetDescription() => $"1~2: {Information12.description}\n" +
                                                     $"3~4: {Information34.description}\n" +
                                                     $"5~6 : {Information56.description}\n";

    /// <summary> �ֻ��� ���� 1 ~ 2�� �ߵ� ȿ�� </summary>
    protected abstract void Use12();

    /// <summary> �ֻ��� ���� 3 ~ 4�� �ߵ� ȿ�� </summary>
    protected abstract void Use34();

    /// <summary> �ֻ��� ���� 5 ~ 6�� �ߵ� ȿ�� </summary>
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
        StartDestroyAnimation();  // �ֻ��� ����
    }
}
