using UnityEngine;

public sealed class EnemyCard020 : CardBase222
{
    protected override string Name => "집 기";

    protected override string Description12 => Description12_(out _, out _);
    protected override string Description34 => Description34_(out _, out _);
    protected override string Description56 => Description56_(out _, out _);

    private string Description12_(out int damage, out int shield)
    {
        damage = 2;
        shield = 5;
        return $"플레이어에게 {damage}의 피해를 주고 {shield}방어도 획득";
    }

    private string Description34_(out int damage, out int shield)
    {
        damage = 3;
        shield = 6;
        return $"플레이어에게 {damage}의 피해를 주고 {shield}방어도 획득";
    }
    private string Description56_(out int damage, out int shield)
    {
        damage = 5;
        shield = 8;
        return $"플레이어에게 {damage}의 피해를 주고 {shield}방어도 획득";
    }

    protected override string Use12()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description12_(out int damage, out int shield);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToShield(shield);
        return description;
    }

    protected override string Use34()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description34_(out int damage, out int shield);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToShield(shield);
        return description;
    }
    protected override string Use56()
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        string description = Description56_(out int damage, out int shield);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToShield(shield);
        return description;
    }
}
