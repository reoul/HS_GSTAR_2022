using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventStage : Stage
{
    [SerializeField]
    private TMP_Text _title, _description;

    public override void StageEnter()
    {
        InitEvent();
    }

    public override void StageExit()
    {
    }

    public override void StageUpdate()
    {
    }

    private void InitEvent()
    {
        _title.text = "";
        _description.text = "";
    }

}
