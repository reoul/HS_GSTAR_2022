using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy001 : Enemy
{
    public override string EnemyName => "사형의 집행자";
    public override int MaxHp => 30;
    public override int StartShield => 0;

    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard006", "EnemyCard007" });
        cardDeck.Add(new List<string> { "EnemyCard008" });
        cardDeck.Add(new List<string> { "EnemyCard009" });
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
