public sealed class PlayerCard009 : CardBase33
{
    protected override string Name => "불안정한 힘";
    protected override string Description123 => Description123_(out _, out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int value1, out int value2)
    {
        value1 = 2;
        value2 = 4;
        return $"플레이어에게 {value1}의 피해를 주고, HP가 가장 낮은 적에게 {value2}의 피해를 줍니다.";
    }
    private string Description456_(out int value)
    {
        value = 4;
        return $"HP가 가장 낮은 적에게 {value}의 피해를 줍니다.";
    }

    protected override void Use123()
    {
        string description = Description123_(out int damage1, out int damage2);

        Logger.Log($"{Name} : 123 : {description}");

        GetOwnerBattleable().ToDamage(damage1);

        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemy = battleManager.GetMinHpEnemy();

        enemy.ToDamage(damage2);
        if (enemy.Hp == 0)
        {
            battleManager.RemoveEnemy(enemy);
            return;
        }
    }

    protected override void Use456()
    {
        string description = Description456_(out int damage);

        Logger.Log($"{Name} : 123 : {description}");

        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemy = battleManager.GetMinHpEnemy();

        enemy.ToDamage(damage);
        if (enemy.Hp == 0)
        {
            battleManager.RemoveEnemy(enemy);
            return;
        }
    }
}
