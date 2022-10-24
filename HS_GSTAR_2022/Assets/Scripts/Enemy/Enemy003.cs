using System.Collections.Generic;
using UnityEngine;

public class Enemy003 : Enemy
{
    public override string EnemyName => "유령 검사";
    public override int MaxHp => 35;
    public override int StartShield => 0;

    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard013", "EnemyCard014" });
        cardDeck.Add(new List<string> { "EnemyCard015" });
        cardDeck.Add(new List<string> { "EnemyCard016" });
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
