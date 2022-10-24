using System.Collections.Generic;
using UnityEngine;

public class Enemy005 : Enemy
{
    public override string EnemyName => "카브라스";
    public override int MaxHp => 25;
    public override int StartShield => 75;


    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard020", "EnemyCard013" });
        cardDeck.Add(new List<string> { "EnemyCard021", "EnemyCard022" });
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
