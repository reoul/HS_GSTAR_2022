using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalDice : Dice
{
    public override void Roll()
    {
        Number = (EDiceNumber) Random.Range((int) EDiceNumber.One, (int) EDiceNumber.Max);
        Play_Ani(Number);
        Debug.Log(Number);
    }

    private void Play_Ani(EDiceNumber num)
    {
        GetComponent<Animator>().Play("Cube|Result_" + ((int)num).ToString(),-1,0f);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }
}
