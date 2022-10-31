using System;

/// <summary> 주사위 눈금 1 ~ 3, 4 ~ 6 발동 카드 </summary>
public class CardBase33 : Card
{
    public string Name { get; set; }
    public sealed override string GetName() => Name;

    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 설명 </summary>
    public string Description123 { get; set; }

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 설명 </summary>
    public string Description456 { get; set; }

    public sealed override string GetDescription() => $"1~3: {Description123}\n" +
                                                      $"4~6: {Description456}\n";

    
    public EventCardEffectType EffectType123 { get; set; }
    public EventCardEffectType EffectType456 { get; set; }
    
    public uint EffectNum123 { get; set; }
    public uint EffectNum456 { get; set; }
    
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
    
    /// <summary> 주사위 눈금 1 ~ 3번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected string Use123()
    {
        EffectApply(EffectType123, (int)EffectNum123);
        return "";
    }

    /// <summary> 주사위 눈금 4 ~ 6번 발동 효과 </summary>
    /// <returns> 발동 효과 설명 </returns>
    protected string Use456()
    {
        
        EffectApply(EffectType456, (int)EffectNum456);
        return "";
    }

    public sealed override void Use(Dice dice)
    {
        string description = "empty";
        switch (dice.Number)
        {
            case EDiceNumber.One:
            case EDiceNumber.Two:
            case EDiceNumber.Three:
                description = Use123();
                break;
            case EDiceNumber.Four:
            case EDiceNumber.Five:
            case EDiceNumber.Six:
                description = Use456();
                break;
            case EDiceNumber.Max:
            default:
                throw new ArgumentOutOfRangeException();
        }

        Logger.Log($"{Name} : {dice} : {description}");
        CardManager.Instance.RemoveCard(this);
        StartDestroyAnimation(); // 카드 삭제
    }
}
