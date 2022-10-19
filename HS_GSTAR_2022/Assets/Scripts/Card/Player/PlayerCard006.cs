public sealed class PlayerCard006 : CardBase33
{
    protected override string Name => "흑마술";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int damage)
    {
        damage = 1;
        return $"플레이어에게 {damage}데미지";
    }
    private string Description456_(out int damage)
    {
        damage = 5;
        return $"HP가 가장 낮은 적에게 {damage}데미지, 만약 대상이 죽었다면 {damage}만큼 회복";
    }

    protected override string Use123()
    {
        string description = Description123_(out int damage);
        GetOwnerBattleable().ToDamage(damage);
        return description;
    }

    protected override string Use456()
    {
        string description = Description456_(out int damage);
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.GetMinHpEnemyList())
        {
            enemy.ToDamage(damage);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
                GetOwnerBattleable().ToHeal(damage);
            }
        }
        return description;
    }

}
