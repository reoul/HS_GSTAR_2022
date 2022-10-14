public sealed class EnemyCard005 : CardBase6
{
    protected override string Name => "급소";
    protected override string Description => "플레이어에게 주사위 눈금수의 2배만큼 데미지";

    protected override void UseCard(Dice dice)
    {
        Logger.Assert(BattleManager.Instance.PlayerBattleable != null);
        
        BattleManager.Instance.PlayerBattleable.ToDamage((int) dice.Number * 2);
    }
}
