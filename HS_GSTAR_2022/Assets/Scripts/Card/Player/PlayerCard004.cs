﻿public sealed class PlayerCard004 : CardBase222
{
    protected override string Name => "베리어";

    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);

    private string Description12_(out int shield)
    {
        shield = 2;
        return $"플레이어에게 {shield}방어도";
    }
    private string Description34_(out int shield)
    {
        shield = 3;
        return $"플레이어에게 {shield}방어도";
    }
    private string Description56_(out int shield)
    {
        shield = 4;
        return $"플레이어에게 {shield}방어도";
    }

    protected override string Use12()
    {
        string description = Description12_(out int shield);
        GetOwnerBattleable().ToShield(shield);
        return description;
    }

    protected override string Use34()
    {
        string description = Description34_(out int shield);
        GetOwnerBattleable().ToShield(shield);
        return description;
    }

    protected override string Use56()
    {
        string description = Description56_(out int shield);
        GetOwnerBattleable().ToShield(shield);
        return description;
    }

}
