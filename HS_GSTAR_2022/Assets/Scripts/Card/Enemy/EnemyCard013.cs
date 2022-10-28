using UnityEngine;

public sealed class EnemyCard013 : CardBase222
{
    protected override string Name => "내려 치기";

    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);


    private string Description12_(out int damage)
    {
        damage = 10;
        return $"플레이어에게 {damage}의 피해";
    }

    private string Description34_(out int damage)
    {
        damage = 20;
        return $"플레이어에게 {damage}의 피해";
    }

    private string Description56_(out int damage)
    {
        damage = 30;
        return $"플레이어에게 {damage}의 피해";
    }            

    protected override string Use12()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description12_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }

    protected override string Use34()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description34_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }

    protected override string Use56()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description56_(out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }
}
