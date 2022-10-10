using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public sealed class Card001 : CardBase222
{
    protected override string Name => "카드001";
    protected override string Description12 => "피가 제일 적은 적에게 2데미지";
    protected override string Description34 => "플레이어에게 3방어";
    protected override string Description56 => "플레이어에게 1회복";
    
    protected override void Use12()
    {
        Logger.Log( $"{Name} : 12 : {Description12}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemyBattle = battleManager.GetMinHpEnemy();
        enemyBattle.ToDamage(2);
        if (enemyBattle.Hp == 0)
        {
            battleManager.RemoveEnemy(enemyBattle);
        }
    }

    protected override void Use34()
    {
        BattleManager battleManager = BattleManager.Instance;
        Logger.Assert(battleManager.PlayerBattleable != null);
        Logger.Log( $"{Name} : 34 : {Description34}");
        battleManager.PlayerBattleable.ToShield(3);
    }

    protected override void Use56()
    {
        BattleManager battleManager = BattleManager.Instance;
        Logger.Assert(battleManager.PlayerBattleable != null);
        Logger.Log( $"{Name} : 56 : {Description56}");
        battleManager.PlayerBattleable.ToHeal(1);
    }
}
