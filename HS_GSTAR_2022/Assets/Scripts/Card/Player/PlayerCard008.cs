public sealed class PlayerCard008 : CardBase6
{
    protected override string Name => "라그나로크";

    protected override string Description => Description_(out _);

    private string Description_(out int damage)
    {
        damage = 2;
        return $"적 전체에게 {damage}의 데미지 (N회 반복)";
    }

    protected override string UseCard(Dice dice)
    {
        string description = Description_(out int damage);
        foreach (IBattleable enemy in BattleManager.Instance.EnemyBattleables)
        {
            enemy.ToDamage(damage * (int) dice.Number);

            if (enemy.Hp == 0)
            {
                BattleManager.Instance.RemoveEnemy(enemy);
            }
        }

        return description;
    }
}
