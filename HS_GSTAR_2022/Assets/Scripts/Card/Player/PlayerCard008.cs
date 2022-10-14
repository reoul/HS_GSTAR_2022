public sealed class PlayerCard008 : CardBase6
{
    protected override string Name => "��׳���ũ";

    protected override string Description => "�� ��ü���� 1�� ������ (Nȸ �ݺ�)";

    protected override void UseCard(Dice dice)
    {
        Logger.Log($"{Name} : {(int)dice.Number} : {GetDescription()}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemy = battleManager.GetMaxHpEnemy();

        for (int i =0; i < (int)dice.Number; i++)
        {
            foreach (IBattleable value in battleManager.EnemyBattleables)
            {
                value.ToDamage(1);

                if (value.Hp == 0)
                {
                    battleManager.RemoveEnemy(enemy);
                }
            }
        }
    }

}
