using System;
using UnityEngine;

[Serializable]
public struct BattleEnemyData
{
    public Enemy enemy;
    public Vector3 position;
    public Vector3 scale;
}

public class BattleStage : Stage
{
    public BattleStageInfo BattleStageInfo { get; set; }

    public Transform EnemyCreatePos;

    public InfoWindow EnemyInfoWindow;
    
    public override void StageEnter()
    {
        //Debug.Assert(BattleStageInfo != null);
        Logger.Log("스테이지 오픈");
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
    }
}
