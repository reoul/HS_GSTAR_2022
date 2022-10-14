public sealed class PlayerCard006 : CardBase33
{
    protected override string Name => "�渶��";

    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int value)
    {
        value = 1;
        return $"�÷��̾�� {value}������";
    }
    private string Description456_(out int value)
    {
        value = 5;
        return $"HP�� ���� ���� ������ {value}������, ���� ����� �׾��ٸ� {value}��ŭ ȸ��";
    }

    protected override void Use123()
    {
        string description = Description123_(out int damage);

        Logger.Log($"{Name} : 123 : {description}");
        GetOwnerBattleable().ToDamage(damage);
    }

    protected override void Use456()
    {
        string description = Description456_(out int damage);

        Logger.Log($"{Name} : 456 : {description}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemyBattle = battleManager.GetMinHpEnemy();
        enemyBattle.ToDamage(damage);

        if (enemyBattle.Hp == 0)
        {
            battleManager.RemoveEnemy(enemyBattle);

            GetOwnerBattleable().ToHeal(damage);
        }
    }

}
