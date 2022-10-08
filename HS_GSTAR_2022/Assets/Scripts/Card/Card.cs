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

    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;

    private void Start()
    {
        gameObject.name = GetName();
        _nameText.text = GetName();
        _contextText.text = GetDescription();
    }

    protected override void ShowOverlay()
    {
        Debug.Log($"{GetName()}의 오버레이 : {GetDescription()}");
    }

    protected override void HideOverlay()
    {
        Debug.Log($"{GetName()}의 오버레이를 숨김");
    }
}
