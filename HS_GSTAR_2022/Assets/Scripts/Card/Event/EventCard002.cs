using System.Collections.Generic;
using UnityEngine.Assertions;

public sealed class EventCard002 : CardBase222
{
    protected override string Name => "생명력 전환";
    protected override string Description12 => Description12_(out _,out _);
    protected override string Description34 => Description34_(out _, out _);
    protected override string Description56 => Description56_(out _, out _);

    private string _description = "체력을 {0} 잃고 최대체력을 {1} 얻습니다.";
    private string Description12_(out int maxHP, out int damage)
    {
        maxHP = 5;
        damage = 20;
        return string.Format(_description, damage, maxHP);
    }
    private string Description34_(out int maxHP, out int damage)
    {
        maxHP = 8;
        damage = 20;
        return string.Format(_description, damage, maxHP);
    }
    private string Description56_(out int maxHP, out int damage)
    {
        maxHP = 10;
        damage = 20;
        return string.Format(_description, damage, maxHP);
    }

    protected override string Use12()
    {
        string description = Description12_(out int maxHP, out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }

    protected override string Use34()
    {
        string description = Description34_(out int maxHP, out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }

    protected override string Use56()
    {
        string description = Description56_(out int maxHP, out int damage);
        BattleManager.Instance.PlayerBattleable.ToDamage(damage);
        return description;
    }
}
