using UnityEngine;

public sealed class EnemyCard029 : CardBase33
{
    protected override string Name => "생기 흡수";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);


    private string Description123_(out int damage)
    {
        damage = 6;
        return $"플레이어에게서 {damage}흡혈";
    }


    private string Description456_(out int damage)
    {
        damage = 12;
        return $"플레이어에게서 {damage}흡혈";
    }            

    protected override string Use123()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description123_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(damage);
        return description;
    }


    protected override string Use456()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description456_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(damage);
        return description;
    }
}
