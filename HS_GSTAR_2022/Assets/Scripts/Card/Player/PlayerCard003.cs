public sealed class PlayerCard003 : CardBase6
{
    protected override string Name => "격노";
    protected override string Description => "HP가 가장 많은 적에게 N만큼 데미지";

    protected override string UseCard(Dice dice)
    {
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.GetMaxHpEnemyList())
        {
            enemy.ToDamage((int)dice.Number);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
            }
        }

        return Description;
    }
}
