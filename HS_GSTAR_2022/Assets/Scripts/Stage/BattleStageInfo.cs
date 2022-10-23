using System;
using UnityEngine;

[Serializable]
public struct StageEnemyInfo
{
    public GameObject EnemyPrefab;
    public Vector3 Position;
}

[CreateAssetMenu(fileName = "BattleStageInfo", menuName = "StageInfo/BattleStageInfo", order = int.MaxValue)]
public class BattleStageInfo : ScriptableObject
{
    [Header("생성 적 카드 타입")]
    public StageEnemyInfo[] StageEnemyInfos;
    
    [Header("플레이어")] 
    public Vector3 PlayerPosition;
    public Vector3 PlayerScale = Vector3.one;
    
    [Header("전투 영역")]
    public Vector3 BattleAreaPosition;
    public Vector3 BattleAreaScale = Vector3.one;
    public Vector2 BattleAreaRect = Vector3.one * 100;
    
    [Header("생성 영역")]
    public Vector3 CardCreatePosition;
    public Vector3 DiceCreatePosition;
}
