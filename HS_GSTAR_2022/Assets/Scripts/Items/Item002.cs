using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item002 : MonoBehaviour
{
    static void ApplyEffect()
    {
        IBattleable playerBattleable = BattleManager.Instance.PlayerBattleable;
        IBattleable EnemyBattleable = BattleManager.Instance.EnemyBattleable;

        EnemyBattleable.ToPiercingDamage(Mathf.FloorToInt(playerBattleable.DefensivePower.FinalStatus * 0.1f));
    }
}
