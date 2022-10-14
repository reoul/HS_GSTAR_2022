public sealed class PlayerCard004 : CardBase222
{
    protected override string Name => "베리어";

    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);

    private string Description12_(out int value)
    {
        value = 2;
        return $"플레이어에게 {value}방어도";
    }
    private string Description34_(out int value)
    {
        value = 3;
        return $"플레이어에게 {value}방어도";
    }
    private string Description56_(out int value)
    {
        value = 4;
        return $"플레이어에게 {value}방어도";
    }

    protected override void Use12()
    {
        string description = Description12_(out int shield);

        Logger.Log($"{Name} : 12 : {description}");
        GetOwnerBattleable().ToShield(shield);
    }

    protected override void Use34()
    {
        string description = Description34_(out int shield);

        Logger.Log($"{Name} : 34 : {description}");
        GetOwnerBattleable().ToShield(shield);
    }

    protected override void Use56()
    {
        string description = Description56_(out int shield);

        Logger.Log($"{Name} : 56 : {description}");
        GetOwnerBattleable().ToShield(shield);
    }

}
