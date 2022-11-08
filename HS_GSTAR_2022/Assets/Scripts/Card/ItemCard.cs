using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private Image _gem;
    [SerializeField] private ItemRatingType _rank;
    [SerializeField] private Sprite[] _gemSprite;
    [SerializeField] private TMP_Text _priceText;

    public ItemInfo ItemCardInfo { get; private set; }

    public bool CanBuy => BattleManager.Instance.PlayerBattleable.OwnerObj.GetComponent<Player>().Money >= ItemCardInfo.Price;

    void Start()
    {
        SetCard(_nameText.text, _contextText.text, _rank);
    }

    public void SetInfo(ItemInfo itemInfo)
    {
        ItemCardInfo = itemInfo;
        SetCard(itemInfo.Name, itemInfo.Description, itemInfo.ratingType);
        GetComponent<BuyItemCard>().Init();
        _priceText.text = itemInfo.Price.ToString();
    }

    public void ApplyItem()
    {
        ItemInfo itemInfo = new ItemInfo(ItemCardInfo);
        switch (ItemCardInfo.EffectInvokeTimeType)
        {
            case ItemEffectInvokeTimeType.BattleStart:
                ApplyItemOfBattleStart(itemInfo);
                break;
            case ItemEffectInvokeTimeType.BattleFinish:
                ApplyItemOfBattleFinish(itemInfo);
                break;
            case ItemEffectInvokeTimeType.AttackFinish:
                ApplyItemOfAttackFinish(itemInfo);
                break;
            case ItemEffectInvokeTimeType.GetItem:
                ApplyItemOfGetType(itemInfo);
                break;
            case ItemEffectInvokeTimeType.Hit:
                ApplyItemOfHit(itemInfo);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetCard(string inputName, string InputContext, ItemRatingType rank)
    {
        _nameText.text = inputName;
        _contextText.text = InputContext;
        _rank = rank;
        _gem.sprite = _gemSprite[(int) rank];
    }

    public string GetCardName()
    {
        return _nameText.text;
    }

    public string GetContext()
    {
        return _contextText.text;
    }

    public ItemRatingType GetRank()
    {
        return _rank;
    }

    private void ApplyItem(ItemInfo itemInfo, bool isItemStatus)
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;

        Debug.Assert(player != null);

        switch (itemInfo.EffectType)
        {
            case ItemEffectType.Heal:
                player.ToHeal(itemInfo.Num);
                break;
            case ItemEffectType.OffensivePower:
                if (isItemStatus)
                {
                    player.OffensivePower.ItemStatus += itemInfo.Num;
                }
                else
                {
                    player.OffensivePower.DefaultStatus += itemInfo.Num;
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(itemInfo.Num, ValueUpdater.valType.pow);
                break;
            case ItemEffectType.PiercingDamage:
                if (isItemStatus)
                {
                    player.PiercingDamage.ItemStatus += itemInfo.Num;
                }
                else
                {
                    player.PiercingDamage.DefaultStatus += itemInfo.Num;
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(itemInfo.Num, ValueUpdater.valType.piercing);
                break;
            case ItemEffectType.DefensivePower:
                if (isItemStatus)
                {
                    player.DefensivePower.ItemStatus += itemInfo.Num;
                }
                else
                {
                    player.DefensivePower.DefaultStatus += itemInfo.Num;
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(itemInfo.Num, ValueUpdater.valType.def);
                break;
            case ItemEffectType.MaxHp:
                player.MaxHp += itemInfo.Num;
                player.Hp += itemInfo.Num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case ItemEffectType.Gold:
                player.OwnerObj.GetComponent<Player>().Money += itemInfo.Num;
                break;
            case ItemEffectType.DoubleDamage:
                BattleManager.IsDoubleDamage = true;
                break;
            case ItemEffectType.Custom:
                itemInfo.itemObj.GetComponent<Item>().Active();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemInfo.EffectType), itemInfo.EffectType, null);
        }
    }

    /// <summary> 전투 시작 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleStart(ItemInfo itemInfo)
    {
        StageManager.Instance.BattleStage.StartBattleEvent.AddListener(() =>
        {
            Debug.Log($"전투 시작 시 {itemInfo.EffectType} {itemInfo.Num} 발동");
            ApplyItem(itemInfo, true);
        });
    }

    /// <summary> 전투 종료 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleFinish(ItemInfo itemInfo)
    {
        StageManager.Instance.BattleStage.FinishBattleEvent.AddListener(() =>
        {
            Debug.Log($"전투 종료 후 {itemInfo.EffectType} {itemInfo.Num} 발동");
            ApplyItem(itemInfo, true);
        });
    }

    /// <summary> 공격 후 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfAttackFinish(ItemInfo itemInfo)
    {
        BattleManager.Instance.PlayerBattleable.FinishAttackEvent.AddListener(() =>
        {
            Debug.Log($"공격 후 {itemInfo.EffectType} {itemInfo.Num} 발동");
            ApplyItem(itemInfo, true);
        });
    }

    /// <summary> 피격 시 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfHit(ItemInfo itemInfo)
    {
        BattleManager.Instance.PlayerBattleable.HitEvent.AddListener(() =>
        {
            Debug.Log($"피격 시 {itemInfo.EffectType} {itemInfo.Num} 발동");
            ApplyItem(itemInfo, true);
        });
    }


    /// <summary> 획득 시 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfGetType(ItemInfo itemInfo)
    {
        Debug.Log($"획득 시 {itemInfo.EffectType} {itemInfo.Num} 발동");
        ApplyItem(itemInfo, true);
    }
}
