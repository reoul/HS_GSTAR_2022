﻿using UnityEngine;

public sealed class EnemyCard002 : CardBase222
{
    protected override string Name => "물어뜯기";
    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);

    private string AttackDescription(int damage) => $"플레이어에게 {damage}데미지";

    private string Description12_(out int damage)
    {
        damage = 2;
        return AttackDescription(damage);
    }

    private string Description34_(out int damage)
    {
        damage = 3;
        return AttackDescription(damage);
    }

    private string Description56_(out int damage)
    {
        damage = 4;
        return AttackDescription(damage);
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
