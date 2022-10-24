using UnityEngine;

public sealed class EnemyCard025 : CardBase222
{
    protected override string Name => "흡혈강타";

    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);


    private string Description12_(out int damage)
    {
        damage = 6;
        return $"플레이어에게서 {damage}흡혈";
    }

    private string Description34_(out int damage)
    {
        damage =9;
        return $"플레이어에게서 {damage}흡혈";
    }

    private string Description56_(out int damage)
    {
        damage = 12;
        return $"플레이어에게서 {damage}흡혈";
    }            

    protected override string Use12()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description12_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(damage);
        return description;
    }

    protected override string Use34()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description34_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(damage);
        return description;
    }

    protected override string Use56()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description56_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(damage);
        return description;
    }
}
