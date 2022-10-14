using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : Stage
{
    public override void StageEnter()
    {
        FindObjectOfType<Player>(true).gameObject.SetActive(true);

        GameObject playerObj = FindObjectOfType<Player>().gameObject;
        Logger.Assert(playerObj != null);
        IBattleable playerBattleable = playerObj.GetComponent<IBattleable>();
        Logger.Assert(playerBattleable != null);

        int createdCardCount = CardManager.Instance.CreateCards(playerBattleable, playerObj, 0.2f);
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
