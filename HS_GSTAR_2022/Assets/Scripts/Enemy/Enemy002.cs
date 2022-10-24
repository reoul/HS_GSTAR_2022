using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy002 : Enemy
{
    public override string EnemyName => "영혼을 먹는자";
    public override int MaxHp => 40;
    public override int StartShield => 0;

    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard010", "EnemyCard011" });
        cardDeck.Add(new List<string> { "EnemyCard012", "EnemyCard009" });
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
