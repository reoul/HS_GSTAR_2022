using UnityEngine;

public sealed class EnemyCard011 : CardBase33
{
    protected override string Name => "세로 활퀴기";

    protected override string Description123 => Description123_(out _, out _);
    protected override string Description456 => Description456_(out _, out _);

    private string Description123_(out int damage, out int heal)
    {
        damage = 10;
        heal = 5;
        return $"플레이어에게 {damage}의 피해를 주고 {heal}회복";
    }

    private string Description456_(out int damage, out int heal)
    {
        damage = 15;
        heal = 10;
        return $"플레이어에게 {damage}의 피해를 주고 {heal}회복";
    }

    protected override string Use123()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description123_(out int damage, out int heal);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

    protected override string Use456()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description456_(out int damage, out int heal);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }
}
