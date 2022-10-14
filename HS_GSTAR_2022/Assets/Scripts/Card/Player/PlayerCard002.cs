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
        damage = 4;
        return $"모든 적에게 {damage}데미지";
    }

    private void AttackAllEnemy(int damage)
    {
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.EnemyBattleables)
        {
            enemy.ToDamage(damage);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
            }
        }
    }
    
    protected override void Use123()
    {
        string description = SetDescription123_(out int damage);
        Logger.Log($"{Name} : 123 : {description}");
        AttackAllEnemy(damage);
    }

    protected override void Use456()
    {
        string description = SetDescription456_(out int damage);
        Logger.Log($"{Name} : 456 : {description}");
        AttackAllEnemy(4);
    }
}
