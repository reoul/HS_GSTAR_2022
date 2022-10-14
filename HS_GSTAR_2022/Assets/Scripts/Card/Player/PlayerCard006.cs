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
