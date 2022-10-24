using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleManager : Singleton<BattleManager>
{
    public IBattleable PlayerBattleable;
    public List<IBattleable> EnemyBattleables;

    [SerializeField] private GameObject _playerPrefab;

    /// <summary> 현재 플레이어 턴인지 </summary>
    private bool _isPlayerTurn;

    private void Awake()
    {
        EnemyBattleables = new List<IBattleable>();

        GameObject playerObj = Instantiate(_playerPrefab, GameObject.Find("Canvas").transform);
        playerObj.SetActive(false);
        PlayerBattleable = playerObj.GetComponent<IBattleable>();
        Debug.Assert(PlayerBattleable != null);
        _isPlayerTurn = false;
    }

    public void Init()
    {
        _isPlayerTurn = false;
        EnemyBattleables = new List<IBattleable>();
    }

    /// <summary> 체력 제일 적은 적들 반환 </summary>
    /// <returns>체력 제일 적은 적 리스트</returns>
    public List<IBattleable> GetMinHpEnemyList()
    {
        Debug.Assert(EnemyBattleables.Count != 0);
        List<IBattleable> minHpEnemyList = new List<IBattleable>();

        minHpEnemyList.Add(EnemyBattleables[0]);
        for (int i = 1; i < EnemyBattleables.Count; ++i)
        {
            if (minHpEnemyList[0].Hp > EnemyBattleables[i].Hp)
            {
                minHpEnemyList.Clear();
                minHpEnemyList.Add(EnemyBattleables[i]);
            }
            else if (minHpEnemyList[0].Hp == EnemyBattleables[i].Hp)
            {
                minHpEnemyList.Add(EnemyBattleables[i]);
            }
        }

        return minHpEnemyList;
    }

    /// <summary> 체력 제일 많은 적 리스트 반환 </summary>
    /// <returns>체력 제일 많은 적 리스트</returns>
    public List<IBattleable> GetMaxHpEnemyList()
    {
        Debug.Assert(EnemyBattleables.Count != 0);
        List<IBattleable> maxHpEnemyList = new List<IBattleable>();

        maxHpEnemyList.Add(EnemyBattleables[0]);
        for (int i = 1; i < EnemyBattleables.Count; ++i)
        {
            if (maxHpEnemyList[0].Hp < EnemyBattleables[i].Hp)
            {
                maxHpEnemyList.Clear();
                maxHpEnemyList.Add(EnemyBattleables[i]);
            }
            else if (maxHpEnemyList[0].Hp == EnemyBattleables[i].Hp)
            {
                maxHpEnemyList.Add(EnemyBattleables[i]);
            }
        }

        return maxHpEnemyList;
    }

    /// <summary> 적 추가 </summary>
    /// <param name="battleable">적 battleable</param>
    public void AddEnemy(IBattleable battleable)
    {
        EnemyBattleables.Add(battleable);
    }

    /// <summary> 적 제거 </summary>
    /// <param name="battleable">적 battleable</param>
    public void RemoveEnemy(IBattleable battleable)
    {
        Debug.Assert(EnemyBattleables.Remove(battleable));
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
        }
    }

    /// <summary> 다음 턴으로 세팅 </summary>
    public void NextTurn()
    {
        _isPlayerTurn = !_isPlayerTurn;
        if (_isPlayerTurn) // 플레이어 턴일 때
        {
            Debug.Assert(PlayerBattleable != null);

            int createdCardCount = CardManager.Instance.CreateCards(PlayerBattleable, PlayerBattleable.OwnerObj,
                Vector3.one * 0.7f, 0.2f);
            Logger.Log($"플레이어 카드 {createdCardCount}장 생성", PlayerBattleable.OwnerObj);
            DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
        }
        else
        {
            List<IUseCard> enemyBattleableList = new List<IUseCard>();
            List<GameObject> cardOwnerList = new List<GameObject>();
            foreach (IBattleable enemy in EnemyBattleables)
            {
                enemyBattleableList.Add(enemy);
                cardOwnerList.Add(enemy.OwnerObj);
            }

            int createdCardCount = CardManager.Instance.CreateCards(enemyBattleableList, cardOwnerList,
                Vector3.one * 0.7f, 0.2f);
            Logger.Log($"적 카드 {createdCardCount}장 생성");
            DiceManager.Instance.CreateDices(createdCardCount, 0.2f);
        }
    }
}
