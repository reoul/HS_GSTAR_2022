using UnityEngine;

public class Item011 : Item
{
    public override void Active()
    {
        IBattleable PlayerBattleable = BattleManager.Instance.PlayerBattleable;

        if(PlayerBattleable.Hp / PlayerBattleable.MaxHp > 50)
        {
            PlayerBattleable.DefensivePower.ItemStatus += 4;
        }
        else
        {
            PlayerBattleable.OffensivePower.ItemStatus += 4;
        }
    }
}
