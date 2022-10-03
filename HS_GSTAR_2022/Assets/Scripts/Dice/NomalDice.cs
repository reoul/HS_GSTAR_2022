using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NomalDice : Dice
{
    public override void Roll()
    {
        Number = (EDiceNumber) Random.Range((int) EDiceNumber.One, (int) EDiceNumber.Max);
        PlayRollAnimation(Number);
    }

    private void PlayRollAnimation(EDiceNumber num)
    {
        GetComponent<Animator>().Play($"Cube|Result_{(int)num}",-1,0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }
}
