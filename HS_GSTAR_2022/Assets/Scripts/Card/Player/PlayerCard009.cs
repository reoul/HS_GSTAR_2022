public sealed class PlayerCard009 : CardBase33
{
    protected override string Name => "불안정한 힘";
    protected override string Description123 => Description123_(out _, out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int playerDamage, out int enemyDamage)
    {
        playerDamage = 2;
        enemyDamage = 4;
        return $"플레이어에게 {playerDamage}의 피해를 주고, HP가 가장 낮은 적에게 {enemyDamage}의 피해를 줍니다.";
    }
    private string Description456_(out int damage)
    {
        damage = 4;
        return $"HP가 가장 낮은 적에게 {damage}의 피해를 줍니다.";
    }

    protected override string Use123()
    {
        string description = Description123_(out int playerDamage, out int enemyDamage);
        
        GetOwnerBattleable().ToDamage(playerDamage);
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.GetMinHpEnemyList())
        {
            enemy.ToDamage(enemyDamage);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
            }
        }
        
        return description;
    }

    protected override string Use456()
    {
        string description = Description456_(out int damage);
        
        foreach (IBattleable enemy in BattleManager.Instance.GetMinHpEnemyList())
        {
            enemy.ToDamage(damage);
            if (enemy.Hp == 0)
            {
                BattleManager.Instance.RemoveEnemy(enemy);
            }
        }
        
        return description;
    }
}
