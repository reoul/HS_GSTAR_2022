using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard003 : CardBase6
{
    protected override string Name => "카드003";
    protected override string Description => "가장 피가 많은 적에게 주사위 눈금 수 만큼 데미지";

    protected override void UseCard(Dice dice)
    {
        Logger.Log($"{Name} : {(int) dice.Number} : {GetDescription()}");
        BattleManager battleManager = BattleManager.Instance;
        IBattleable enemy = battleManager.GetMaxHpEnemy();
        enemy.ToDamage((int) dice.Number);
        if (enemy.Hp == 0)
        {
            battleManager.RemoveEnemy(enemy);
        }
    }
}
