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

        // 플레이어 설정
        GameObject playerObj = BattleManager.Instance.PlayerBattleable.OwnerObj;
        playerObj.transform.localPosition = BattleStageInfo.PlayerPosition;
        playerObj.SetActive(true);
        Logger.Log("스테이지 오픈");

        // 영역 설정
        GameObject BattleAreaObj = GameObject.Find("BattleAreaImg");
        BattleAreaObj.transform.localPosition = BattleStageInfo.BattleAreaPosition;
        BattleAreaObj.transform.localScale = BattleStageInfo.BattleAreaScale;
        BattleAreaObj.GetComponent<RectTransform>().sizeDelta = BattleStageInfo.BattleAreaRect;
        
        // 적 생성
        Transform enemyParent = GameObject.Find("EnemyParent").transform;
        foreach (StageEnemyInfo enemyInfo in BattleStageInfo.StageEnemyInfos)
        {
            Debug.Assert(enemyInfo.EnemyPrefab != null);
            
            GameObject enemyObj = Instantiate(enemyInfo.EnemyPrefab, enemyParent);
            enemyObj.transform.localPosition = enemyInfo.Position;
            
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
        Destroy(this.gameObject);
    }
}
