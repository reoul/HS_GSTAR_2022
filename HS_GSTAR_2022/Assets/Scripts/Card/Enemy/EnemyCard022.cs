using UnityEngine;

public sealed class EnemyCard022 : CardBase33
{
    protected override string Name => "철갑";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int shield)
    {
        shield = 10;
        return $"{shield}방어도 획득";
    }

    private string Description456_(out int shield)
    {
        shield = 15;
        return $" {shield}방어도 획득";
    }

    protected override string Use123()
    {
        string description = Description123_(out int shield);

        //GetOwnerBattleable().ToShield(shield);
        return description;
    }

    protected override string Use456()
    {
        string description = Description456_(out int shield);

        //GetOwnerBattleable().ToShield(shield);
        return description;
    }
}
