using UnityEngine;

public class Item009 : Item
{
    public override void Active()
    {
        if (0.03f >= Random.RandomRange(0, 1))
        {
            IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

            EnemyBattleable.ToPiercingDamage(9999);
        }
    }
}
