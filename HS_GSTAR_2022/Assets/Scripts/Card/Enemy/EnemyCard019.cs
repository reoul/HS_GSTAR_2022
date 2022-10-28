using UnityEngine;

public sealed class EnemyCard019 : CardBase33
{
    protected override string Name => "바위 던지기";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int damage)
    {
        damage = 50;
        return $"플레이어에게 {damage}의 피해";
    }

    private string Description456_(out int damage)
    {
        damage = 60;
        return $"플레이어에게 {damage}의 피해";
    }

    protected override string Use123()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description123_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }

    protected override string Use456()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description456_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }
}
