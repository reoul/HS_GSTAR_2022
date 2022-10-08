using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card001 : CardBase222
{
    protected override string Name => "카드001";
    protected override string Description12 => "적에게 2데미지";
    protected override string Description34 => "플레이어에게 3방어";
    protected override string Description56 => "플레이어에게 1회복";
    
    protected override void Use12()
    {
        Debug.Log( $"{Name} : 12 : {Description12}");
    }

    protected override void Use34()
    {
        Debug.Log( $"{Name} : 34 : {Description34}");
    }

    protected override void Use56()
    {
        Debug.Log( $"{Name} : 56 : {Description56}");
    }
}
