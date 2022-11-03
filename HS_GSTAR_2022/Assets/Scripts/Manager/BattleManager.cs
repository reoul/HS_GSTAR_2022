using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private Player _player;
    public IBattleable PlayerBattleable { get; private set; }
    public IBattleable EnemyBattleable { get; private set; }
    
    /// <summary> 공격 애니메이션이 끝났는지 여부 </summary>
    public bool FinishAttack { get; set; }

    private void Awake()
    {
        Debug.Assert(_player != null);
        
        PlayerBattleable = _player.GetComponent<IBattleable>();
        _player.Init();
    }

    /// <summary> 적 설정 </summary>
    /// <param name="battleable">적 battleable</param>
    public void SetEnemy(IBattleable battleable)
    {
        EnemyBattleable = battleable;
    }

    /// <summary> 전투 시작 </summary>
    public void StartBattle()
    {
        StartCoroutine(BattleCoroutine());
    }

    private IEnumerator BattleCoroutine()
    {
        while (true)
        {
            FinishAttack = false;
            // 플레이어 공격
            PlayerBattleable.StartAttackAnimation();
            
            while (!FinishAttack)
            {
                yield return null;
            }
            if (EnemyBattleable.Hp == 0)
            {
                break;
            }
            
            FinishAttack = false;
            // 적 공격
            Logger.Log("적 공격 시작");
            EnemyBattleable.StartAttackAnimation();

            while (!FinishAttack)
            {
                yield return null;
            }
            if (PlayerBattleable.Hp == 0)
            {
                break;
            }
        }
    }
}
