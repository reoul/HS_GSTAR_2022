using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public Stage stage;
    private void OnMouseUp()
    {
        if (!FadeManager.Instance.IsFading)
        {
            FadeManager.Instance.FadeInStartEvent.AddListener(Test);
            FadeManager.Instance.StartFadeOut();
        }
    }

    private void Test()
    {
        GameObject.Find("Map").SetActive(false);
        StageManager.Instance.OpenStage(stage);
    }
}
