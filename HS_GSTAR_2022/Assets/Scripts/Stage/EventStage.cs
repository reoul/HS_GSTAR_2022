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
        //Debug.Assert(EventStageInfo != null);

        Logger.Log("스테이지 오픈");

        //_cardParent.localPosition = EventStageInfo.CardCreatePosition;
        //_diceParent.localPosition = EventStageInfo.DiceCreatePosition;
    }

    public override void StageExit()
    {
        //FindObjectOfType<Player>().gameObject.SetActive(false);
    }

    public override void StageUpdate()
    {
    }

    public void InitEvent(string title, string description)
    {
        _title.text = title;
        _description.text = description;
    }

    public void CreateEventCards()
    {
        foreach (EventCardInfo cardInfo in EventStageInfo.EventCardInfos)
        {
            //cardInfo.
        }
    }
}
