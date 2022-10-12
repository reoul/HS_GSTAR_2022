using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public override void StageEnter()
    {
        FindObjectOfType<Player>(true).gameObject.SetActive(true);
        int createdCardCount = CardManager.Instance.CreateCards(BattleManager.Instance.PlayerBattleable, 0.2f);
        DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
        CardManager.Instance.RemoveAllCard();
        DiceManager.Instance.RemoveAllDice();
        FindObjectOfType<Player>().gameObject.SetActive(false);
    }
}
