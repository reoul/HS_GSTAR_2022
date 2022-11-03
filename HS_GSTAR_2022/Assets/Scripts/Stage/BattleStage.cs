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
    
    public bool IsFinishBattle { get; set; }

    private float _battleTime;
    
    public override void StageEnter()
    {
        Logger.Log("스테이지 오픈");
        Time.timeScale = 1;
        IsFinishBattle = false;
        _battleTime = 0;
    }

    public override void StageUpdate()
    {
        if (IsFinishBattle)
        {
            Time.timeScale = 1;
        }
        else
        {
            _battleTime += Time.deltaTime;
            Time.timeScale = 1 + Mathf.Min(_battleTime, 2f);
        }
        
    }

    public override void StageExit()
    {
        Time.timeScale = 1;
        IsFinishBattle = false;
        _battleTime = 0;
    }
}
