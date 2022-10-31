using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 주사위 눈금 1 ~ 6 발동 카드 </summary>
public class CardBase6 : Card
{
    public string Name { get; set; }
    public sealed override string GetName() => Name;
    
    /// <summary> 모든 주사위 눈금 발동 효과 설명 </summary>
    public string Description { get; set; }

    public sealed override string GetDescription() => Description;
    
    public EventCardEffectType EventCardEffectType { get; set; }

    /// <summary> 모든 주사위 눈금 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    private void UseCard(Dice dice)
    {
        switch (EventCardEffectType)
        {
            case EventCardEffectType.AddOffensivePower:
                BattleManager.Instance.PlayerBattleable.OffensivePower += (int)dice.Number;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal((int)dice.Number, ValueUpdater.valType.pow);
                }
                break;
            case EventCardEffectType.SubOffensivePower:
                BattleManager.Instance.PlayerBattleable.OffensivePower -= (int)dice.Number;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(-(int)dice.Number, ValueUpdater.valType.pow);
                }
                break;
            case EventCardEffectType.AddPiercingDamage:
                BattleManager.Instance.PlayerBattleable.PiercingDamage += (int)dice.Number;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>())
                {
                    updater.AddVal((int)dice.Number, ValueUpdater.valType.piercing);
                }
                break;
            case EventCardEffectType.SubPiercingDamage:
                BattleManager.Instance.PlayerBattleable.PiercingDamage -= (int)dice.Number;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(-(int)dice.Number, ValueUpdater.valType.piercing);
                }
                break;
            case EventCardEffectType.AddMaxHp:
                BattleManager.Instance.PlayerBattleable.MaxHp += (int)dice.Number;
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.SubMaxHp:
                BattleManager.Instance.PlayerBattleable.MaxHp -= (int)dice.Number;
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.AddHp:
                BattleManager.Instance.PlayerBattleable.ToHeal((int)dice.Number);
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.SubHp:
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage((int)dice.Number);
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.AddDefensivePower:
                BattleManager.Instance.PlayerBattleable.DefensivePower += (int)dice.Number;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal((int)dice.Number, ValueUpdater.valType.def);
                }
                break;
            case EventCardEffectType.SubDefensivePower:
                BattleManager.Instance.PlayerBattleable.DefensivePower -= (int)dice.Number;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(-(int)dice.Number, ValueUpdater.valType.def);
                }
                break;
            case EventCardEffectType.NoEffect:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public sealed override void Use(Dice dice)
    {
        UseCard(dice);
        Logger.Log($"{Name} : {dice} : {Description}");
        CardManager.Instance.RemoveCard(this);
        StartDestroyAnimation();  // 카드 삭제
    }
}
