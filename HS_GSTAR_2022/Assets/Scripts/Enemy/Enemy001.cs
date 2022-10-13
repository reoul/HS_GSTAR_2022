using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy001 : Enemy
{
    public override int MaxHp => 100;

    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> {"EnemyCard001", "EnemyCard002", "EnemyCard003"});
        cardDeck.Add(new List<string> {"EnemyCard004", "EnemyCard005"});
        int rand = Random.Range(0, cardDeck.Count + 1);
        return cardDeck[rand];
    }
}
