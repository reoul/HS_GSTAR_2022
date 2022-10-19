using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public override void StageEnter()
    {
        BattleManager.Instance.PlayerBattleable.OwnerObj.SetActive(true);
        BattleManager.Instance.NextTurn();
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
        CardManager.Instance.RemoveAllCard();
        DiceManager.Instance.RemoveAllDice();
        BattleManager.Instance.PlayerBattleable.OwnerObj.SetActive(false);
    }
}
