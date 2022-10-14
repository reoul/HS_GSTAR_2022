public sealed class EnemyCard003 : CardBase33
{
    protected override string Name => "휴식";
    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string HealDescription(int heal) => $"체력 {heal}회복";
    
    private string Description123_(out int heal)
    {
        heal = 2;
        return HealDescription(heal);
    }
    
    private string Description456_(out int heal)
    {
        heal = 4;
        return HealDescription(heal);
    }
    
    protected override void Use123()
    {
        Description123_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
    }

    protected override void Use456()
    {
        Description456_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
    }
}
