using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class NomalDice : Dice
{
    public override void Roll()
    {
        Number = (EDiceNumber) Random.Range((int) EDiceNumber.One, (int) EDiceNumber.Max);
        GetComponentInChildren<Animator>().Play($"Roll{(int)Number}",-1,0f);  // 굴리는 애니메이션 호출
    }
}
