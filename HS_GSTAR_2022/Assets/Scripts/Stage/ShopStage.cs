using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStage : Stage
{
    public ItemInfo[] ItemInfoArray { get; set; }

    public override void StageEnter()
    {
    }

    public override void StageUpdate()
    {
    }

    public override void StageExit()
    {
    }

    public void ApplyItem(ItemEffectType effectType, int num)
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;

        Debug.Assert(player != null);

        switch (effectType)
        {
            case ItemEffectType.Heal:
                player.ToHeal(num);
                break;
            case ItemEffectType.OffensivePower:
                player.OffensivePower.ItemStatus += num;
                player.InfoWindow.UpdateOffensivePowerText(player.OffensivePower.FinalStatus);
                break;
            case ItemEffectType.PiercingDamage:
                player.PiercingDamage.ItemStatus += num;
                player.InfoWindow.UpdatePiercingDamageText(player.PiercingDamage.FinalStatus);
                break;
            case ItemEffectType.DefensivePower:
                player.DefensivePower.ItemStatus += num;
                player.InfoWindow.UpdateDefensivePowerText(player.DefensivePower.FinalStatus);
                break;
            case ItemEffectType.MaxHp:
                player.MaxHp += num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case ItemEffectType.Gold:
                FindObjectOfType<Player>().Money += num;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }

    /// <summary> 전투 시작 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleStart(ItemEffectType effectType, int num)
    {
        StageManager.Instance.BattleStage.StartBattleEvent.AddListener(() =>
        {
            ApplyItem(effectType, num);
        });
    }

    /// <summary> 전투 종료 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleFinish(ItemEffectType effectType, int num)
    {
        StageManager.Instance.BattleStage.FinishBattleEvent.AddListener(() =>
        {
            ApplyItem(effectType, num);
        });
    }

    /// <summary> 공격 후 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfAttackFinish(ItemEffectType effectType, int num)
    {
        BattleManager.Instance.PlayerBattleable.FinishAttackEvent.AddListener(() =>
        {
            ApplyItem(effectType, num);
        });
    }

    /// <summary> 획득 시 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfGetType(ItemEffectType effectType, int num)
    {
        switch (effectType)
        {
            case ItemEffectType.Heal:
                break;
            case ItemEffectType.OffensivePower:
                break;
            case ItemEffectType.PiercingDamage:
                break;
            case ItemEffectType.DefensivePower:
                break;
            case ItemEffectType.MaxHp:
                break;
            case ItemEffectType.Gold:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(effectType), effectType, null);
        }
    }
}
