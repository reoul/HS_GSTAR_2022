using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Status
{
    private int _defaultStatus;

    public int DefaultStatus
    {
        get => _defaultStatus;
        set => _defaultStatus = value > 0 ? value : 0;
    }
    
    public int ItemStatus { get; set; }
    public int FinalStatus => DefaultStatus + ItemStatus;
}

public sealed class Player : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;

    private int _maxHp;
    public int MaxHp
    {
        get => _maxHp;
        set
        {
            _maxHp = value;
            Hp = _maxHp > Hp ? Hp : MaxHp;
        }
    }

    private int _hp;
    public int Hp
    {
        get => _hp;
        set => _hp = value > 0 ? value : 0;
    }
    
    public Status OffensivePower { get; set; }
    public Status DefensivePower { get; set; }
    public Status PiercingDamage { get; set; }


    [SerializeField] private InfoWindow _infoWindow;
    private Animator _animator;

    public InfoWindow InfoWindow
    {
        get { return _infoWindow; }
        set { _infoWindow = value; }
    }

    public ValueUpdater ValueUpdater { get; private set; }
    
    public UnityEvent FinishAttackEvent { get; set; }

    [SerializeField] private TMP_Text _moneyText; 
    
    private int _money;
    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            _moneyText.text = value.ToString();
        }
    }

    public void Init()
    {
        OffensivePower = new Status();
        DefensivePower = new Status();
        PiercingDamage = new Status();
        
        MaxHp = 100;
        Hp = MaxHp;
        OffensivePower.DefaultStatus = 10;
        DefensivePower.DefaultStatus = 10;
        PiercingDamage.DefaultStatus = 10;
        
        _animator = GetComponent<Animator>();
        _infoWindow.UpdateHpBar(Hp, MaxHp);
        _infoWindow.UpdateOffensivePowerText(OffensivePower.FinalStatus);
        _infoWindow.UpdateDefensivePowerText(DefensivePower.FinalStatus);
        _infoWindow.UpdatePiercingDamageText(PiercingDamage.FinalStatus);
        
        ValueUpdater = FindObjectOfType<ValueUpdater>(true);
        FinishAttackEvent = new UnityEvent();
        Money = 0;
    }

    /// <summary> 공격 애니메이션에서 호출 (삭제 금지) </summary>
    public void AttackEnemy()
    {
        IBattleable enemy = BattleManager.Instance.EnemyBattleable;
        enemy.ToDamage(OffensivePower.FinalStatus);
        enemy.ToPiercingDamage(PiercingDamage.FinalStatus);

        SoundManager.Instance.PlaySound("MP_AttackSound");

        if (enemy.Hp != 0)
        {
            enemy.StartHitAnimation();
        }
        else
        {
            enemy.StartDeadAnimation();
        }
    }

    public void ToDamage(int damage)
    {
        Logger.Assert(_infoWindow != null);

        damage = damage >= DefensivePower.FinalStatus ? damage - DefensivePower.FinalStatus : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp.ToString()}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Logger.Assert(_infoWindow != null);

        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 관통 데미지 {piercingDamage} 입음. 현재 체력 {Hp.ToString()}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        Logger.Assert(_infoWindow != null);

        DefensivePower.DefaultStatus = defensivePower;
        _infoWindow.UpdateDefensivePowerText(DefensivePower.FinalStatus);
        Logger.Log($"플레이어 방어력 {defensivePower}로 설정됨", gameObject);
    }

    public void ToHeal(int heal)
    {
        Logger.Assert(_infoWindow != null);

        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void StartAttackAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger("Attack");
    }

    public void StartHitAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger("Hit");
    }

    public void StartDeadAnimation()
    {
        Logger.Assert(_animator != null);

        _animator.SetTrigger("Death");
    }

    /// <summary> 공격 애니메이션 끝났을 때 호출 (삭제 금지) </summary>
    public void FinishAttackAnimation()
    {
        // 공격 후 이벤트 발동
        FinishAttackEvent.Invoke();
        
        BattleManager.Instance.FinishAttack = true;
    }

    /// <summary> Death 애니메이션 끝났을 때 호출 (삭제 금지) </summary>
    public void FinishDeathAnimation()
    {
        Destroy(gameObject);
        FindObjectOfType<BattleStage>().IsFinishBattle = true;
        SceneManager.LoadScene(0);
    }
}
