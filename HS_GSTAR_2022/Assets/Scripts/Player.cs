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
    public int LastAttackDamage { get; set; }


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
            _money = value > 0 ? value : 0;
            _moneyText.text = value.ToString();
        }
    }

    public void Init()
    {
        OffensivePower = new Status();
        DefensivePower = new Status();
        PiercingDamage = new Status();

        MaxHp = 300;
        Hp = MaxHp;
        OffensivePower.DefaultStatus = 5;
        PiercingDamage.DefaultStatus = 5;
        DefensivePower.DefaultStatus = 20;

        _animator = GetComponent<Animator>();
        _infoWindow.UpdateHpBar(Hp, MaxHp);

        ValueUpdater = FindObjectOfType<ValueUpdater>(true);

        ValueUpdater.Init();

        ValueUpdater.AddVal(OffensivePower.DefaultStatus, ValueUpdater.valType.pow, false);
        ValueUpdater.AddVal(PiercingDamage.DefaultStatus, ValueUpdater.valType.piercing, false);
        ValueUpdater.AddVal(DefensivePower.DefaultStatus, ValueUpdater.valType.def, false);

        FinishAttackEvent = new UnityEvent();
        Money = 1000;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToHeal(100);
        }
    }

    /// <summary> 공격 애니메이션에서 호출 (삭제 금지) </summary>
    public void AttackEnemy()
    {
        IBattleable enemy = BattleManager.Instance.EnemyBattleable;
        
        int damage = OffensivePower.FinalStatus * (BattleManager.IsDoubleDamage ? 2 : 1);
        LastAttackDamage = damage;
        enemy.ToDamage(damage);
        enemy.ToPiercingDamage(PiercingDamage.FinalStatus);

        SoundManager.Instance.PlaySound("AttackSound");

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
        Time.timeScale = 1;
        FadeManager.Instance.StartFadeOut();
        StageManager.Instance.SetFadeEvent(StageType.GameOver);
    }
}
