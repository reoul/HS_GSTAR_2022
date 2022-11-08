using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventStage : Stage
{
    [SerializeField] private TMP_Text _title, _description;

    [SerializeField] private Transform _cardParent, _diceParent;

    public EventStageInfo EventStageInfo { get; set; }

    public override void StageEnter()
    {
        Logger.Log("스테이지 오픈");
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
