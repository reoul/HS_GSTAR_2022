using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventStageInfo", menuName = "StageInfo/EventStageInfo", order = int.MaxValue)]
public class EventStageInfo : ScriptableObject
{
    [Header("생성 이벤트 카드 타입")]
    public string[] EventCardTypes;
    
    [Header("전투 영역")]
    public Vector3 BattleAreaPosition;
    public Vector3 BattleAreaScale;
    
    [Header("생성 영역")]
    public Vector3 CardCreatePosition;
    public Vector3 DiceCreatePosition;

    [Header("스테이지 제목")]
    public string Title;
    [Header("스테이지 설명")]
    public string Description;
    [Header("플레이어 위치")]
    public Vector3 PlayerLocation;
}
