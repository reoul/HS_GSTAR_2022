public sealed class PlayerCard003 : CardBase6
{
    protected override string Name => "격노";
    protected override string Description => "HP가 가장 많은 적에게 N만큼 데미지";

    protected override void UseCard(Dice dice)
    {
        Logger.Log($"{Name} : {(int) dice.Number} : {GetDescription()}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemy = battleManager.GetMaxHpEnemy();
        enemy.ToDamage((int) dice.Number);
        if (enemy.Hp == 0)
        {
            battleManager.RemoveEnemy(enemy);
        }
    }
}
