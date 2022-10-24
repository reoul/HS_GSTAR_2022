using System.Collections.Generic;
using UnityEngine;

public class Enemy004 : Enemy
{
    public override string EnemyName => "골렘 가디언";
    public override int MaxHp => 80;
    public override int StartShield => 0;

    protected override List<string> GetCharacterCardCodes()
    {
        List<List<string>> cardDeck = new List<List<string>>();
        cardDeck.Add(new List<string> { "EnemyCard017", "EnemyCard018", "EnemyCard019" });
        int rand = Random.Range(0, cardDeck.Count);
        return cardDeck[rand];
    }
}
