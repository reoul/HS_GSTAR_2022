using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct BattleEnemyData
{
    public Enemy enemy;
    public Vector3 position;
    public Vector3 scale;
}

public class BattleStage : Stage
{
    public Transform EnemyCreatePos;

    public InfoWindow EnemyInfoWindow;
    
    public bool IsFinishBattle { get; set; }

    public bool IsPlayerWin { get; set; }
    
    private float _battleTime;
    
    /// <summary> 전투 시작 시 발동 </summary>
    public UnityEvent StartBattleEvent { get; set; }
    
    /// <summary> 전투 종료 시 발동 </summary>
    public UnityEvent FinishBattleEvent { get; set; }
    
    public override void StageEnter()
    {
        Logger.Log("스테이지 오픈");

        IBattleable player = BattleManager.Instance.PlayerBattleable;
        player.OffensivePower.ItemStatus = 0;
        player.PiercingDamage.ItemStatus = 0;
        player.DefensivePower.ItemStatus = 0;

        // 전투 시작 아이템 발동
        StartBattleEvent.Invoke();
        
        Time.timeScale = 1;
        IsFinishBattle = false;
        IsPlayerWin = false;
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
        IBattleable player = BattleManager.Instance.PlayerBattleable;
        
        // 전투 종료 아이템 발동
        if (IsPlayerWin)
        {
            ValueUpdater valueUpdater = player.InfoWindow.GetComponent<ValueUpdater>();
            valueUpdater.AddVal(-player.OffensivePower.ItemStatus, ValueUpdater.valType.pow);
            valueUpdater.AddVal(-player.PiercingDamage.ItemStatus, ValueUpdater.valType.piercing);
            valueUpdater.AddVal(-player.DefensivePower.ItemStatus, ValueUpdater.valType.def);
        
            player.OffensivePower.ItemStatus = 0;
            player.PiercingDamage.ItemStatus = 0;
            player.DefensivePower.ItemStatus = 0;
            
            FinishBattleEvent.Invoke();
        }
        
        Time.timeScale = 1;
        IsFinishBattle = false;
        IsPlayerWin = false;
        _battleTime = 0;
    }
}
