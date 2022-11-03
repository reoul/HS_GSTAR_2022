using System;
using UnityEngine;
using TMPro;

public abstract class Card : OverlayBase
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _contextText;
    [SerializeField] private GameObject _cardShowObj;
    [SerializeField] private Animator _cardEffectAnimator;
    
    /// <summary> 카드 이름 반환 </summary>
    /// <returns>카드 이름</returns>
    public abstract string GetName();
    
    /// <summary> 발동 효과 설명 반환 </summary>
    /// <returns>발동 효과 설명</returns>
    public abstract string GetDescription();
    
    /// <summary> 카드 효과 사용 </summary>
    /// <param name="dice">주사위</param>
    public abstract void Use(Dice dice);

    private void Awake()
    {
        _cardShowObj.SetActive(false);
    }

    private void Start()
    {
        Create();
    }

    /// <summary> 카드 처음 생성될 때 호출 </summary>
    public void Create()
    {
        gameObject.name = GetName();
        _nameText.text = GetName();
        _contextText.text = GetDescription();
        
        _cardEffectAnimator.SetTrigger("Create");   // 생성 애니메이션 호출
    }

    /// <summary> 카드 삭제 될 때 호출 </summary>
    public void StartDestroyAnimation()
    {
        GetComponent<BoxCollider>().enabled = false;
        _cardEffectAnimator.SetTrigger("Destroy");  // 삭제 애니메이션 호출
    }

    protected override void ShowOverlay()
    {
        Logger.Log($"{GetName()}의 오버레이 : {GetDescription()}");
    }
    
    protected override void HideOverlay()
    {
        Logger.Log($"{GetName()}의 오버레이를 숨김");
    }

    /// <summary> 효과 적용 </summary>
    /// <param name="effectType">효과 타입</param>
    /// <param name="num">수치</param>
    protected void ApplyEffect(EventCardEffectType effectType, int num)
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;
        ValueUpdater valueUpdater = FindObjectOfType<Player>(true).ValueUpdater;
        
        Debug.Assert(player != null);
        Debug.Assert(valueUpdater != null);
        
        switch (effectType)
        {
            case EventCardEffectType.AddOffensivePower:
                player.OffensivePower.DefaultStatus += num;
                valueUpdater.AddVal(num, ValueUpdater.valType.pow);
                break;
            case EventCardEffectType.SubOffensivePower:
                int oldOffensivePower = player.OffensivePower.DefaultStatus;
                player.OffensivePower.DefaultStatus -= num;
                valueUpdater.AddVal(player.OffensivePower.DefaultStatus > 0 ? -num : -oldOffensivePower, ValueUpdater.valType.pow);
                break;
            case EventCardEffectType.AddPiercingDamage:
                player.PiercingDamage.DefaultStatus += num;
                valueUpdater.AddVal(num, ValueUpdater.valType.piercing);
                break;
            case EventCardEffectType.SubPiercingDamage:
                int oldPiercingDamage = player.PiercingDamage.DefaultStatus;
                player.PiercingDamage.DefaultStatus -= num;
                valueUpdater.AddVal(player.PiercingDamage.DefaultStatus > 0 ? -num : -oldPiercingDamage, ValueUpdater.valType.piercing);
                break;
            case EventCardEffectType.AddMaxHp:
                player.MaxHp += num;
                player.Hp += num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case EventCardEffectType.SubMaxHp:
                player.MaxHp -= num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case EventCardEffectType.AddHp:
                player.ToHeal(num);
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case EventCardEffectType.SubHp:
                player.ToPiercingDamage(num);
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);
                break;
            case EventCardEffectType.AddDefensivePower:
                player.DefensivePower.DefaultStatus += num;
                valueUpdater.AddVal(num, ValueUpdater.valType.def);
                break;
            case EventCardEffectType.SubDefensivePower:
                int oldDefensivePower = player.DefensivePower.DefaultStatus;
                player.DefensivePower.DefaultStatus -= num;
                valueUpdater.AddVal(player.DefensivePower.DefaultStatus > 0 ? -num : -oldDefensivePower, ValueUpdater.valType.def);
                break;
            case EventCardEffectType.NoEffect:
                break;
            case EventCardEffectType.AddMoney:
                SoundManager.Instance.PlaySound("MP_Coin Drop(mp3cut.net)");
                player.OwnerObj.GetComponent<Player>().Money += num;
                break;
            case EventCardEffectType.SubMoney:
                SoundManager.Instance.PlaySound("MP_동전 짤랑이는 소리");
                player.OwnerObj.GetComponent<Player>().Money -= num;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
