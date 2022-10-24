using System.Collections.Generic;

public sealed class PlayerCard002 : CardBase33
{
    protected override string Name => "불사지르기";
    protected override string Description123 => SetDescription123_(out _);
    protected override string Description456 => SetDescription456_(out _);


    private string SetDescription123_(out int damage)
    {
        damage = 3;
        return $"모든 적에게 {damage}데미지";
    }

    private string SetDescription456_(out int damage)
    {
        damage = 100;
        return $"모든 적에게 {damage}데미지";
    }

    private void AttackAllEnemy(int damage)
    {
        BattleManager battleManager = BattleManager.Instance;
        List<IBattleable> removeEnemyList = new List<IBattleable>();
        foreach (IBattleable enemy in battleManager.EnemyBattleables)
        {
            enemy.ToDamage(damage);
            if (enemy.Hp == 0)
            {
                removeEnemyList.Add(enemy);
            }
        }

        foreach (IBattleable enemy in removeEnemyList)
        {
            battleManager.RemoveEnemy(enemy);
        }
    }

    protected override string Use123()
    {
        string description = SetDescription123_(out int damage);
        AttackAllEnemy(damage);
        return description;
    }

    protected override string Use456()
    {
        string description = SetDescription456_(out int damage);
        AttackAllEnemy(damage);
        return description;
    }
}
