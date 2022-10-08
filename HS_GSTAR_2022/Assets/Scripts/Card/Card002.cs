using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card002 : CardBase33
{
    protected override string Name => "카드002";
    protected override string Description123 => "모든 적에게 3데미지";
    protected override string Description456 => "다른 능력";
    
    protected override void Use123()
    {
        Logger.Log($"{Name} : 123 : {Description123}");
    }

    protected override void Use456()
    {
        Logger.Log($"{Name} : 456 : {Description456}");
    }
}
