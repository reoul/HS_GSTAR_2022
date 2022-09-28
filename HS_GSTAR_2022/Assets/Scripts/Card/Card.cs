using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour, IOverlayable
{
    public abstract string GetName();
    public abstract string GetDescription();
    public abstract void Use(Dice dice);
    
    public void ShowOverlay()
    {
    }
}
