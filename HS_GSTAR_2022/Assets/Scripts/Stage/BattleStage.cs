using System;
using UnityEngine;

[Serializable]
public struct Data
{
    public Enemy enemy;
    public Vector3 position;
    public Vector3 scale;
}

public class BattleStage : Stage
{
    public BattleStageInfo BattleStageInfo { get; set; }
    [SerializeField] private Data[] _enemies;
    
    public override void StageEnter()
    {
        BattleManager.Instance.PlayerBattleable.OwnerObj.SetActive(true);
        
        /*foreach (Enemy enemy in _enemies)
        {
            Logger.Assert(enemy != null);
            BattleManager.Instance.AddEnemy(enemy);
            Logger.Log($"{enemy.OwnerObj.name} 적 추가");
        }*/
        
        BattleManager.Instance.NextTurn();
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
        CardManager.Instance.RemoveAllCard();
        DiceManager.Instance.RemoveAllDice();
        BattleManager.Instance.PlayerBattleable.OwnerObj.SetActive(false);
    }
}
