using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleManager : Singleton<BattleManager>
{
    public IBattleable PlayerBattleable;
    public IBattleable EnemyBattleable;

    [SerializeField] private GameObject _playerPrefab;

    private void Awake()
    {
        GameObject playerObj = Instantiate(_playerPrefab, GameObject.Find("Canvas").transform);
        playerObj.SetActive(false);
        PlayerBattleable = playerObj.GetComponent<IBattleable>();
        Debug.Assert(PlayerBattleable != null);
    }

    /// <summary> 적 설정 </summary>
    /// <param name="battleable">적 battleable</param>
    public void SetEnemy(IBattleable battleable)
    {
        EnemyBattleable = battleable;
    }

    /// <summary> 적 제거 </summary>
    /// <param name="battleable">적 battleable</param>
    public void RemoveEnemy(IBattleable battleable)
    {
        /*Logger.Assert(EnemyBattleables.Remove(battleable));
        Destroy(battleable.OwnerObj);
        if (EnemyBattleables.Count == 0)
        {
            Logger.Log("모든 적이 제거되었습니다.");
            // todo : 전투 종료 로직 구현
            FadeManager.Instance.FadeInStartEvent.AddListener(() =>
            {
                StageManager.Instance.OpenStage(FindObjectOfType<Map>(true));
                GameObject.Find("MapManager").transform.GetChild(0).gameObject.SetActive(true);
            });
            FadeManager.Instance.StartFadeOut();
        }*/
    }

    /// <summary> 전투 시작 </summary>
    public void StartBattle()
    {
        StartCoroutine(BattleCoroutine());
    }

    private IEnumerator BattleCoroutine()
    {
        WaitForSeconds waitOneSecond = new WaitForSeconds(1);
        while (true)
        {
            // 플레이어 공격
            EnemyBattleable.ToDamage(PlayerBattleable.OffensivePower);
            EnemyBattleable.ToPiercingDamage(PlayerBattleable.PiercingDamage);

            yield return waitOneSecond;
            if (EnemyBattleable.Hp == 0)
            {
                break;
            }
            
            // 적 공격
            PlayerBattleable.ToDamage(EnemyBattleable.OffensivePower);
            PlayerBattleable.ToPiercingDamage(EnemyBattleable.PiercingDamage);

            yield return waitOneSecond;
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
