public sealed class PlayerCard007 : CardBase6
{
    protected override string Name => "소각";

    protected override string Description => "HP가 가장 높은 적에게 2의 데미지 (N회 반복)";

    protected override void UseCard(Dice dice)
    {
        Logger.Log($"{Name} : {(int)dice.Number} : {GetDescription()}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemy = battleManager.GetMaxHpEnemy();

        for (int i =0; i < (int)dice.Number; i++)
        {
            enemy.ToDamage(1);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
                return;
            }
        }
    }
}
