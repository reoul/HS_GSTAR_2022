using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleManager : Singleton<BattleManager>
{
    public Player Player;
    public IBattleable PlayerBattleable;
    public IBattleable EnemyBattleable;

    [SerializeField] private GameObject _playerPrefab;

    public bool FinishAttack;

    private void Awake()
    {
        PlayerBattleable = Player.GetComponent<IBattleable>();
        PlayerBattleable.OwnerObj.GetComponent<Player>().Init();
        Debug.Assert(PlayerBattleable != null);
        Time.timeScale = 1;
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
                yield return new WaitForEndOfFrame();
            }
            if (EnemyBattleable.Hp == 0)
            {
                break;
            }
            
            FinishAttack = false;
            // 적 공격
            EnemyBattleable.StartAttackAnimation();

            while (!FinishAttack)
            {
                yield return new WaitForEndOfFrame();
            }
            if (PlayerBattleable.Hp == 0)
            {
                break;
            }
        }
    }

    /// <summary> 다음 턴으로 세팅 </summary>
    public void NextTurn()
    {
    }
}
