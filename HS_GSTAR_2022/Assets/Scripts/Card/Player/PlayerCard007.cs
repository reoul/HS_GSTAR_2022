public sealed class PlayerCard007 : CardBase6
{
    protected override string Name => "소각";

    protected override string Description => Description_(out _);

    private string Description_(out int damage)
    {
        damage = 2;
        return $"HP가 가장 높은 적에게 {damage}의 데미지 (N회 반복)";
    }

    protected override string UseCard(Dice dice)
    {
        Description_(out int damage);
        foreach (IBattleable enemy in BattleManager.Instance.GetMaxHpEnemyList())
        {
            enemy.ToDamage(damage * (int) dice.Number);
            if (enemy.Hp == 0)
            {
                BattleManager.Instance.RemoveEnemy(enemy);
            }
        }

        return Description;
    }
}
