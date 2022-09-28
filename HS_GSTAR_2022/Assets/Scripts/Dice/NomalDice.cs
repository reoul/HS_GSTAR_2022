using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalDice : Dice
{
    public override void Roll()
    {
        Number = (EDiceNumber) Random.Range((int) EDiceNumber.One, (int) EDiceNumber.Max);
    }
}
