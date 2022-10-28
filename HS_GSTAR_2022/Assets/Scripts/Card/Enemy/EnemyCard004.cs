public sealed class EnemyCard004 : CardBase6
{
    protected override string Name => "웅크리기";
    protected override string Description => "방어도를 주사위 눈금수만큼 얻음";

    protected override string UseCard(Dice dice)
    {
        //GetOwnerBattleable().ToShield((int) dice.Number);
        return Description;
    }
}
