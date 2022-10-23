public sealed class EnemyCard003 : CardBase33
{
    protected override string Name => "휴식";
    protected override string Description123 => Description123_(out _);
    protected override string Description456 => Description456_(out _);

    private string Description123_(out int heal)
    {
        heal = 2;
        return $"체력 {heal}회복";
    }
    
    private string Description456_(out int heal)
    {
        heal = 4;
        return $"체력 {heal}회복";
    }
    
    protected override string Use123()
    {
        string description = Description123_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }

    protected override string Use456()
    {
        string description = Description456_(out int heal);
        GetOwnerBattleable().ToHeal(heal);
        return description;
    }
}
