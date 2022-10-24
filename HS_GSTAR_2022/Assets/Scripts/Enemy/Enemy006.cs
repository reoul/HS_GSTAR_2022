using System.Collections.Generic;
using UnityEngine;

public class Enemy006 : Enemy
{
    public override string EnemyName => "캡푸";
    public override int MaxHp => 125;
    public override int StartShield => 25;


    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard013"});
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
