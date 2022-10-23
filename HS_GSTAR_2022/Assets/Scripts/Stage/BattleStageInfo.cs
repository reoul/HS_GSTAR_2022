using System;
using UnityEngine;

[Serializable]
public struct StageEnemyInfo
{
    public string EnemyType;
    public Vector3 Position;
    public Vector3 Scale;
}

[CreateAssetMenu(fileName = "BattleStageInfo", menuName = "StageInfo/BattleStageInfo", order = int.MaxValue)]
public class BattleStageInfo : ScriptableObject
{
    [Header("생성 적 카드 타입")]
    public StageEnemyInfo[] StageEnemyInfos;
    [Header("전투 영역")]
    public Vector3 BattleAreaPosition;
    public Vector3 BattleAreaScale;
    [Header("생성 영역")]
    public Vector3 CardCreatePosition;
    public Vector3 DiceCreatePosition;
}
