public sealed class PlayerCard005 : CardBase222
{
    protected override string Name => "회복";

    protected override string Description12 => SetDescription12_(out _);
    protected override string Description34 => SetDescription34_(out _);
    protected override string Description56 => SetDescription56_(out _);

    private string SetDescription12_(out int heal)
    {
        heal = 1;
        return $"플레이어 HP {heal}회복";
    }
    private string SetDescription34_(out int heal)
    {
        heal = 2;
        return $"플레이어 HP {heal}회복";
    }
    private string SetDescription56_(out int heal)
    {
        heal = 3;
        return $"플레이어 HP {heal}회복";
    }

    protected override string Use12()
    {
        string description = SetDescription12_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

    protected override string Use34()
    {
        string description = SetDescription34_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

    protected override string Use56()
    {
        string description = SetDescription56_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

}
