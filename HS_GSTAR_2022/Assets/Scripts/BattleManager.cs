using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BattleManager : Singleton<BattleManager>
{
    public IBattleable PlayerBattleable { get; private set; }
    public List<IBattleable> EnemyBattleables { get; private set; }

    [SerializeField] private GameObject _playerPrefab;

    private void Awake()
    {
        EnemyBattleables = new List<IBattleable>();

        GameObject playerObj = Instantiate(_playerPrefab);
        playerObj.SetActive(false);
        PlayerBattleable = playerObj.GetComponent<IBattleable>();
        Logger.Assert(PlayerBattleable != null);
    }

    public IBattleable GetMinHpEnemy()
    {
        Logger.Assert(EnemyBattleables.Count != 0);
        IBattleable enemy = EnemyBattleables[0];
        foreach (IBattleable battleable in EnemyBattleables)
        {
            if (enemy.Hp > battleable.Hp)
            {
                enemy = battleable;
            }
        }

        return enemy;
    }

    public IBattleable GetMaxHpEnemy()
    {
        Logger.Assert(EnemyBattleables.Count != 0);
        IBattleable enemy = EnemyBattleables[0];
        foreach (IBattleable battleable in EnemyBattleables)
        {
            if (enemy.Hp > battleable.Hp)
            {
                enemy = battleable;
            }
        }

        return enemy;
    }

    public void AddEnemy(IBattleable battleable)
    {
        EnemyBattleables.Add(battleable);
    }

    public void RemoveEnemy(IBattleable battleable)
    {
        Logger.Assert(EnemyBattleables.Remove(battleable));
    }
}
