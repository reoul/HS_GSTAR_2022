using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<Stage> _stages;
    [SerializeField] private Stage _curStage;

    public void OpenStage(Stage stage)
    {
        Logger.Assert(stage != null);
        _curStage?.StageExit();
        stage.StageEnter();
        _curStage = stage;
    }
}
