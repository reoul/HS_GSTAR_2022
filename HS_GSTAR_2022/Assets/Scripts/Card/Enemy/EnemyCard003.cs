public class EnemyCard003 : CardBase33
{
    protected override string Name { get; }
    protected override string Description123 => Description123_();
    protected override string Description456 => Description456_();

    private string Description123_()
    {
        return "";
    }
    
    private string Description456_()
    {
        return "";
    }
    
    protected override void Use123()
    {
        throw new System.NotImplementedException();
    }

    protected override void Use456()
    {
        throw new System.NotImplementedException();
    }
}
