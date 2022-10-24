using System.Collections.Generic;
using UnityEngine;

public class Enemy008 : Enemy
{
    public override string EnemyName => "유령기사";
    public override int MaxHp => 300;
    public override int StartShield => 25;


    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard027", "EnemyCard028" });
        cardDeck.Add(new List<string> { "EnemyCard029"});
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
