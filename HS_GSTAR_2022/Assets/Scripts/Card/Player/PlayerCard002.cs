using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard002 : CardBase33
{
    protected override string Name => "카드002";
    protected override string Description123 => "모든 적에게 3데미지";
    protected override string Description456 => "모든 적에게 4데미지";
    
    protected override void Use123()
    {
        Logger.Log($"{Name} : 123 : {Description123}");
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.EnemyBattleables)
        {
            enemy.ToDamage(3);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
            }
        }
    }

    protected override void Use456()
    {
        Logger.Log($"{Name} : 456 : {Description456}");
        BattleManager battleManager = BattleManager.Instance;
        foreach (IBattleable enemy in battleManager.EnemyBattleables)
        {
            enemy.ToDamage(4);
            if (enemy.Hp == 0)
            {
                battleManager.RemoveEnemy(enemy);
            }
        }
    }
}
