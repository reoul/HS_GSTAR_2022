using UnityEngine;
using System.Collections.Generic;

public sealed class EnemyCard001 : CardBase222
{
    protected override string Name => "할퀴기";
    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _, out _);

    enum stringK
    {
        Damage,
        Heal
    }

    private List<string> Descriptions = new List<string>
    {
        "플레이어에게 {0}데미지",
        "체력 {0}회복"
    };

    private string AttackDescription(int damage) => $"플레이어에게 {damage}데미지";

    private string HealDescription(int heal) => $"체력 {heal}회복";
    
    private string Description12_(out int damage)
    {
        damage = 2;
        return string.Format(Descriptions[(int)stringK.Damage], damage);
    }

    private string Description34_(out int damage)
    {
        damage = 3;
        return AttackDescription(damage);
    }

    private string Description56_(out int damage, out int heal)
    {
        damage = 3;
        heal = 4;
        return $"{AttackDescription(damage)}, {HealDescription(heal)}";
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
        
        string description = Description56_(out int damage, out int heal);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }
}
