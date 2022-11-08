using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventStage : Stage
{
    [SerializeField] private TMP_Text _title, _description;
    
    public override void StageEnter()
    {
        Logger.Log("이벤트 스테이지 입장 로직 시작");
        Logger.Log("이벤트 스테이지 입장 로직 종료");
    }

    public override void StageExit()
    {
        Logger.Log("이벤트 스테이지 퇴장 로직 시작");
        Logger.Log("이벤트 스테이지 퇴장 로직 종료");
    }

    public override void StageUpdate()
    {
    }

    public void InitEvent(string title, string description)
    {
        Logger.Log($"이벤트 스테이지 [{title} : {description}] 으로 설정");
        _title.text = title;
        _description.text = description;
    }
}
