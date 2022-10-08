using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : OverlayBase
{
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract void Use(Dice dice);

    protected override void ShowOverlay()
    {
        Debug.Log($"{GetName()}의 오버레이 : {GetDescription()}");
    }

    protected override void HideOverlay()
    {
        Debug.Log($"{GetName()}의 오버레이를 숨김");
    }
}
