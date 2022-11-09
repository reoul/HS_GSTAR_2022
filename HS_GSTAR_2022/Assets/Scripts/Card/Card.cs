using System;
using UnityEngine;
using TMPro;

public abstract class Card : OverlayBase
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private GameObject _cardShowObj;
    [SerializeField] private Animator _cardEffectAnimator;

    /// <summary> 카드 이름 반환 </summary>
    /// <returns>카드 이름</returns>
    public abstract string GetName();

    /// <summary> 발동 효과 설명 반환 </summary>
    /// <returns>발동 효과 설명</returns>
    public abstract string GetDescription();

    /// <summary> 카드 설명 적용 </summary>
    public abstract void SetContentText();

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
        //_contextText.text = GetDescription();
        SetContentText();

        _cardEffectAnimator.SetTrigger("Create"); // 생성 애니메이션 호출
    }

    /// <summary> 카드 삭제 될 때 호출 </summary>
    public void StartDestroyAnimation()
    {
        GetComponent<BoxCollider>().enabled = false;
        _cardEffectAnimator.SetTrigger("Destroy"); // 삭제 애니메이션 호출
        SoundManager.Instance.PlaySound("CardDownDice2");
    }

    protected override void ShowOverlay()
    {
        //Logger.Log($"{GetName()}의 오버레이 : {GetDescription()}");
    }

    protected override void HideOverlay()
    {
        //Logger.Log($"{GetName()}의 오버레이를 숨김");
    }

    /// <summary> 효과 적용 </summary>
    /// <param name="effectType">효과 타입</param>
    /// <param name="num">수치</param>
    protected string ApplyEffect(EventCardEffectType effectType, int num)
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;
        ValueUpdater valueUpdater = FindObjectOfType<Player>(true).ValueUpdater;

        Debug.Assert(player != null);
        Debug.Assert(valueUpdater != null);

        // 효과 설명 문자열
        string effectDescription;
        // 이전 스텟 관련 정보 문자열
        string previousStatusStr;

        switch (effectType)
        {
            case EventCardEffectType.AddOffensivePower:
                previousStatusStr = player.OffensivePower.ToString();

                player.OffensivePower.DefaultStatus += num;
                valueUpdater.AddVal(num, ValueUpdater.valType.pow);

                effectDescription = $"공격력 기본스텟 {num} 증가, \n이전 {previousStatusStr}\n이후 {player.OffensivePower}";
                break;
            case EventCardEffectType.SubOffensivePower:
                previousStatusStr = player.OffensivePower.ToString();

                int oldOffensivePower = player.OffensivePower.DefaultStatus;
                player.OffensivePower.DefaultStatus -= num;
                valueUpdater.AddVal(player.OffensivePower.DefaultStatus > 0 ? -num : -oldOffensivePower, ValueUpdater.valType.pow);

                effectDescription = $"공격력 기본스텟 {num} 감소, \n이전 {previousStatusStr}\n이후 {player.OffensivePower}";
                break;
            case EventCardEffectType.AddPiercingDamage:
                previousStatusStr = player.PiercingDamage.ToString();

                player.PiercingDamage.DefaultStatus += num;
                valueUpdater.AddVal(num, ValueUpdater.valType.piercing);

                effectDescription = $"관통데미지 기본스텟 {num} 증가, \n이전 {previousStatusStr}\n이후 {player.PiercingDamage}";
                break;
            case EventCardEffectType.SubPiercingDamage:
                previousStatusStr = player.PiercingDamage.ToString();

                int oldPiercingDamage = player.PiercingDamage.DefaultStatus;
                player.PiercingDamage.DefaultStatus -= num;
                valueUpdater.AddVal(player.PiercingDamage.DefaultStatus > 0 ? -num : -oldPiercingDamage, ValueUpdater.valType.piercing);

                effectDescription = $"관통데미지 기본스텟 {num} 감소, \n이전 {previousStatusStr}\n이후 {player.PiercingDamage}";
                break;
            case EventCardEffectType.AddMaxHp:
                previousStatusStr = $"체력/최대체력 : {player.Hp}/{player.MaxHp}";

                player.MaxHp += num;
                player.Hp += num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);

                effectDescription = $"최대체력 {num} 증가, \n이전 {previousStatusStr}\n이후 체력/최대체력 : {player.Hp}/{player.MaxHp}";
                break;
            case EventCardEffectType.SubMaxHp:
                previousStatusStr = $"체력/최대체력 : {player.Hp}/{player.MaxHp}";

                player.MaxHp -= num;
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);

                effectDescription = $"최대체력 {num} 감소, \n이전 {previousStatusStr}\n이후 체력/최대체력 : {player.Hp}/{player.MaxHp}";
                break;
            case EventCardEffectType.AddHp:
                previousStatusStr = $"체력/최대체력 : {player.Hp}/{player.MaxHp}";

                player.ToHeal(num);
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);

                effectDescription = $"체력 {num} 증가, \n이전 {previousStatusStr}\n이후 체력/최대체력 : {player.Hp}/{player.MaxHp}";
                break;
            case EventCardEffectType.SubHp:
                previousStatusStr = $"체력/최대체력 : {player.Hp}/{player.MaxHp}";

                player.ToPiercingDamage(num);
                player.InfoWindow.UpdateHpBar(player.Hp, player.MaxHp);

                effectDescription = $"체력 {num} 감소, \n이전 {previousStatusStr}\n이후 체력/최대체력 : {player.Hp}/{player.MaxHp}";
                break;
            case EventCardEffectType.AddDefensivePower:
                previousStatusStr = player.DefensivePower.ToString();

                player.DefensivePower.DefaultStatus += num;
                valueUpdater.AddVal(num, ValueUpdater.valType.def);

                effectDescription = $"방어력 기본스텟 {num} 증가, \n이전 {previousStatusStr}\n이후 {player.DefensivePower}";
                break;
            case EventCardEffectType.SubDefensivePower:
                previousStatusStr = player.DefensivePower.ToString();

                int oldDefensivePower = player.DefensivePower.DefaultStatus;
                player.DefensivePower.DefaultStatus -= num;
                valueUpdater.AddVal(player.DefensivePower.DefaultStatus > 0 ? -num : -oldDefensivePower, ValueUpdater.valType.def);

                effectDescription = $"방어력 기본스텟 {num} 감소, \n이전 {previousStatusStr}\n이후 {player.DefensivePower}";
                break;
            case EventCardEffectType.NoEffect:
                effectDescription = $"아무 효과 없음";
                break;
            case EventCardEffectType.AddMoney:
            {
                Player player_ = player.OwnerObj.GetComponent<Player>();
                previousStatusStr = $"돈 : {player_.Money}";

                SoundManager.Instance.PlaySound("CoinSound");
                player_.Money += num;

                effectDescription = $"돈 {num} 증가, \n이전 {previousStatusStr}\n이후 돈 : {player_.Money}";
            }
                break;
            case EventCardEffectType.SubMoney:
            {
                Player player_ = player.OwnerObj.GetComponent<Player>();
                previousStatusStr = $"돈 : {player_.Money}";

                SoundManager.Instance.PlaySound("CoinSound");
                player.OwnerObj.GetComponent<Player>().Money -= num;

                effectDescription = $"돈 {num} 감소, \n이전 {previousStatusStr}\n이후 돈 : {player_.Money}";
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (player.Hp == 0)
        {
            Logger.Log("아이템 사용 중에 HP가 0이 되어 게임오버 되었습니다.");
            FadeManager.Instance.StartFadeOut();
            StageManager.Instance.SetFadeEvent(StageType.GameOver);
        }

        return effectDescription;
    }
}
