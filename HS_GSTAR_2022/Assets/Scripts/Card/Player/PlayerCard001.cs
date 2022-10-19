using System.Collections.Generic;
using UnityEngine.Assertions;

public sealed class PlayerCard001 : CardBase222
{
    protected override string Name => "화염구";
    protected override string Description12 => Description12_(out _);
    protected override string Description34 => Description34_(out _);
    protected override string Description56 => Description56_(out _);

    private string _description = "HP가 가장 적은 적에게 {0}데미지";
    private string Description12_(out int damage)
    {
        damage = 2;
        return string.Format(_description, damage);
    }
    private string Description34_(out int damage)
    {
        damage = 3;
        return string.Format(_description, damage);
    }
    private string Description56_(out int damage)
    {
        damage = 4;
        return string.Format(_description, damage);
    }

    private void Attack(int damage)
    {
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.GetMinHpEnemyList())
        {
            enemy.ToDamage(damage);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
            }
        }
    }

    protected override string Use12()
    {
        string description = Description12_(out int damage);
        Attack(damage);
        return description;
    }

    protected override string Use34()
    {
        string description = Description34_(out int damage);
        Attack(damage);
        return description;
    }

    protected override string Use56()
    {
        string description = Description56_(out int damage);
        Attack(damage);
        return description;
    }
}
