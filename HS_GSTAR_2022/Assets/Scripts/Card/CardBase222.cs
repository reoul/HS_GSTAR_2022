using System;

/// <summary> 주사위 눈금 1 ~ 2, 3 ~ 4, 5 ~ 6 발동 카드 </summary>
public class CardBase222 : Card
{
    public string Name { get; set; }
    public sealed override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 2번에 발동 효과 설명 </summary>
    public string Description12 { get; set; }

    /// <summary> 주사위 눈금 3 ~ 4번에 발동 효과 설명 </summary>
    public string Description34 { get; set;  }

    /// <summary> 주사위 눈금 5 ~ 6번에 발동 효과 설명 </summary>
    public string Description56 { get; set;  }

    public sealed override string GetDescription() => $"1~2: {Description12}\n" +
                                                      $"3~4: {Description34}\n" +
                                                      $"5~6 : {Description56}\n";

    public EventCardEffectType EffectType12 { get; set; }
    public EventCardEffectType EffectType34 { get; set; }
    public EventCardEffectType EffectType56 { get; set; }
    
    public uint EffectNum12 { get; set; }
    public uint EffectNum34 { get; set; }
    public uint EffectNum56 { get; set; }

    private void EffectApply(EventCardEffectType type, int num)
    {
        switch (type)
        {
            case EventCardEffectType.AddOffensivePower:
                BattleManager.Instance.PlayerBattleable.OffensivePower += num;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(num, ValueUpdater.valType.pow);
                }
                break;
            case EventCardEffectType.SubOffensivePower:
                BattleManager.Instance.PlayerBattleable.OffensivePower -= num;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(-num, ValueUpdater.valType.pow);
                }
                break;
            case EventCardEffectType.AddPiercingDamage:
                BattleManager.Instance.PlayerBattleable.PiercingDamage += num;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(num, ValueUpdater.valType.piercing);
                }
                break;
            case EventCardEffectType.SubPiercingDamage:
                BattleManager.Instance.PlayerBattleable.PiercingDamage -= num;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(-num, ValueUpdater.valType.piercing);
                }
                break;
            case EventCardEffectType.AddMaxHp:
                BattleManager.Instance.PlayerBattleable.MaxHp += num;
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.SubMaxHp:
                BattleManager.Instance.PlayerBattleable.MaxHp -= num;
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.AddHp:
                BattleManager.Instance.PlayerBattleable.ToHeal(num);
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.SubHp:
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(num);
                BattleManager.Instance.PlayerBattleable.ToPiercingDamage(0);
                break;
            case EventCardEffectType.AddDefensivePower:
                BattleManager.Instance.PlayerBattleable.DefensivePower += num;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(num, ValueUpdater.valType.def);
                }
                break;
            case EventCardEffectType.SubDefensivePower:
                BattleManager.Instance.PlayerBattleable.DefensivePower -= num;
                foreach (ValueUpdater updater in FindObjectsOfType<ValueUpdater>(true))
                {
                    updater.AddVal(-num, ValueUpdater.valType.def);
                }
                break;
            case EventCardEffectType.NoEffect:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    /// <summary> 주사위 눈금 1 ~ 2번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected string Use12()
    {
        EffectApply(EffectType12, (int)EffectNum12);
        return "";
    }

    /// <summary> 주사위 눈금 3 ~ 4번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected string Use34()
    {
        EffectApply(EffectType34, (int)EffectNum34);
        
        return "";
    }

    /// <summary> 주사위 눈금 5 ~ 6번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected string Use56()
    {
        EffectApply(EffectType56, (int)EffectNum56);
        
        return "";
    }

    public sealed override void Use(Dice dice)
    {
        string description;
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
                description = Use12();
                break;
            case EDiceNumber.Three:
            case EDiceNumber.Four:
                description = Use34();
                break;
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                description = Use56();
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Logger.Log($"{Name} : {dice} : {description}");
        CardManager.Instance.RemoveCard(this);
        StartDestroyAnimation();  // 카드 삭제
    }
}
