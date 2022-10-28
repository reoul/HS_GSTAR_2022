using UnityEngine;

public sealed class EnemyCard016 : CardBase6
{
    protected override string Name => "회피";
    protected override string Description => "플레이어에게 2의 피해 (N회 반복)";

    protected override string UseCard(Dice dice)
    {
        Debug.Assert(BattleManager.Instance.PlayerBattleable != null);

        for (int i=0;i< (int)dice.Number; i++)
        {
            BattleManager.Instance.PlayerBattleable.ToDamage(2);
        }
        return Description;
    }
}
