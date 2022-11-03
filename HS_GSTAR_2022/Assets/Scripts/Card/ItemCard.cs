using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private Image _gem;
    [SerializeField] private ItemRatingType _rank;
    [SerializeField] private Sprite[] _gemSprite;
    [SerializeField] private TMP_Text _priceText;

    public ItemInfo ItemInfo { get; private set; }

    public bool CanBuy => BattleManager.Instance.PlayerBattleable.OwnerObj.GetComponent<Player>().Money >= ItemInfo.Price;

    void Start()
    {
        SetCard(_nameText.text, _contextText.text, _rank);
    }

    public void SetInfo(ItemInfo itemInfo)
    {
        ItemInfo = itemInfo;
        SetCard(itemInfo.Name, itemInfo.Description, itemInfo.ratingType);
        GetComponent<BuyItemCard>().Init();
        _priceText.text = itemInfo.Price.ToString();
    }

    public void ApplyItem()
    {
        switch (ItemInfo.EffectInvokeTimeType)
        {
            case ItemEffectInvokeTimeType.BattleStart:
                ApplyItemOfBattleStart(ItemInfo.EffectType, ItemInfo.Num);
                break;
            case ItemEffectInvokeTimeType.BattleFinish:
                ApplyItemOfBattleFinish(ItemInfo.EffectType, ItemInfo.Num);
                break;
            case ItemEffectInvokeTimeType.AttackFinish:
                ApplyItemOfAttackFinish(ItemInfo.EffectType, ItemInfo.Num);
                break;
            case ItemEffectInvokeTimeType.GetItem:
                ApplyItemOfGetType(ItemInfo.EffectType, ItemInfo.Num);
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

    private void ApplyItem(ItemEffectType effectType, int num, bool isItemStatus)
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;

        Debug.Assert(player != null);

        switch (effectType)
        {
            case ItemEffectType.Heal:
                player.ToHeal(num);
                break;
            case ItemEffectType.OffensivePower:
                if (isItemStatus)
                {
                    player.OffensivePower.ItemStatus += num;
                }
                else
                {
                    player.OffensivePower.DefaultStatus += num;
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(num, ValueUpdater.valType.pow);
                break;
            case ItemEffectType.PiercingDamage:
                if (isItemStatus)
                {
                    player.PiercingDamage.ItemStatus += num;
                }
                else
                {
                    player.PiercingDamage.DefaultStatus += num;
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(num, ValueUpdater.valType.piercing);
                break;
            case ItemEffectType.DefensivePower:
                if (isItemStatus)
                {
                    player.DefensivePower.ItemStatus += num;
                }
                else
                {
                    player.DefensivePower.DefaultStatus += num;
                }

                player.OwnerObj.GetComponent<Player>().ValueUpdater.AddVal(num, ValueUpdater.valType.def);
                break;
            case ItemEffectType.MaxHp:
                player.MaxHp += num;
                player.Hp += num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case ItemEffectType.Gold:
                player.OwnerObj.GetComponent<Player>().Money += num;
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
            Debug.Log($"전투 시작 시 {effectType} {num} 발동");
            ApplyItem(effectType, num, true);
        });
    }

    /// <summary> 전투 종료 시에 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfBattleFinish(ItemEffectType effectType, int num)
    {
        StageManager.Instance.BattleStage.FinishBattleEvent.AddListener(() =>
        {
            Debug.Log($"전투 종료 후 {effectType} {num} 발동");
            ApplyItem(effectType, num, false);
        });
    }

    /// <summary> 공격 후 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfAttackFinish(ItemEffectType effectType, int num)
    {
        BattleManager.Instance.PlayerBattleable.FinishAttackEvent.AddListener(() =>
        {
            Debug.Log($"공격 후 {effectType} {num} 발동");
            ApplyItem(effectType, num, true);
        });
    }

    /// <summary> 획득 시 발동되는 아이템 적용 </summary>
    /// <param name="effectType">발동 효과 타입</param>
    /// <param name="num">수치</param>
    private void ApplyItemOfGetType(ItemEffectType effectType, int num)
    {
        Debug.Log($"획득 시 {effectType} {num} 발동");
        ApplyItem(effectType, num, false);
    }
}
