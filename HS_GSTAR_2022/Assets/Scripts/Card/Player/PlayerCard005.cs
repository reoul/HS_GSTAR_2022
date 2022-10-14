public sealed class PlayerCard005 : CardBase222
{
    protected override string Name => "ȸ��";

    protected override string Description12 => SetDescription12_(out _);
    protected override string Description34 => SetDescription34_(out _);
    protected override string Description56 => SetDescription56_(out _);

    private string SetDescription12_(out int value)
    {
        value = 1;
        return $"�÷��̾� HP {value}ȸ��";
    }
    private string SetDescription34_(out int value)
    {
        value = 2;
        return $"�÷��̾� HP {value}ȸ��";
    }
    private string SetDescription56_(out int value)
    {
        value = 3;
        return $"�÷��̾� HP {value}ȸ��";
    }

    protected override void Use12()
    {
        string description = SetDescription12_(out int heal);

        Logger.Log($"{Name} : 12 : {description}");
        GetOwnerBattleable().ToHeal(heal);
    }

    protected override void Use34()
    {
        string description = SetDescription34_(out int heal);

        Logger.Log($"{Name} : 34 : {description}");
        GetOwnerBattleable().ToHeal(heal);
    }

    protected override void Use56()
    {
        string description = SetDescription56_(out int heal);

        Logger.Log($"{Name} : 56 : {description}");
        GetOwnerBattleable().ToHeal(heal);
    }

}
