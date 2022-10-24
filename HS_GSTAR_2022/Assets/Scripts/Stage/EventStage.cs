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
    }

    public override void StageExit()
    {
    }

    public override void StageUpdate()
    {
    }

    public void InitEvent(string title, string description)
    {
        _title.text = title;
        _description.text = description;
    }

}
