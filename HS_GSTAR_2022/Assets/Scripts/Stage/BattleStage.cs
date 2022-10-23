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
    
    public override void StageEnter()
    {
        Debug.Assert(BattleStageInfo != null);
        
        BattleManager.Instance.PlayerBattleable.OwnerObj.SetActive(true);
        Logger.Log("스테이지 오픈");

        Transform enemyParent = GameObject.Find("EnemyParent").transform;
        
        // 적 생성
        foreach (StageEnemyInfo enemyInfo in BattleStageInfo.StageEnemyInfos)
        {
            Type enemyType = Type.GetType($"{enemyInfo.EnemyType},Assembly-CSharp");
            Debug.AssertFormat(enemyType != null, gameObject, "{0} 은 존재하지 않는 적 타입입니다", enemyType);
            
            GameObject enemyObj = new GameObject();
            enemyObj.transform.parent = enemyParent;
            enemyObj.transform.localPosition = enemyInfo.Position;
            enemyObj.transform.localScale = enemyInfo.Scale;
            enemyObj.AddComponent(enemyType);
            
            BattleManager.Instance.AddEnemy(enemyObj.GetComponent<IBattleable>());
            Logger.Log($"{enemyObj.name} 적 추가");
        }
        
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
