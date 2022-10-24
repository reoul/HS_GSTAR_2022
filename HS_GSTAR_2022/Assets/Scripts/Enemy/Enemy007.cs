using System.Collections.Generic;
using UnityEngine;

public class Enemy007 : Enemy
{
    public override string EnemyName => "타우로스";
    public override int MaxHp => 200;
    public override int StartShield => 0;


    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard023", "EnemyCard024", "EnemyCard025" });
        cardDeck.Add(new List<string> { "EnemyCard026"});
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
