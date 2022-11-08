using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item001 : MonoBehaviour
{
    static void ApplyEffect()
    {
        IBattleable playerBattleable = BattleManager.Instance.PlayerBattleable;

        playerBattleable.ToHeal(Mathf.FloorToInt(playerBattleable.LastAttackDamage * 0.1f));
    }
}
