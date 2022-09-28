using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDiceNumber
{
    One = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Max
}

public abstract class Dice : MonoBehaviour
{
    public EDiceNumber Number { get; protected set; }

    public abstract void Roll();
}
