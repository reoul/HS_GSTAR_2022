using UnityEngine;

public sealed class EnemyCard012 : CardBase33
{
    protected override string Name => "세로 활퀴기";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int damage)
    {
        damage = 20;
        return $"플레이어에게 {damage}의 피해";
    }

    private string Description456_(out int damage)
    {
        damage = 40;
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
