using System;
using UnityEngine;

public sealed class Player : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;

    private int _maxHp;

    public int MaxHp
    {
        get { return _maxHp; }
        set
        {
            _maxHp = value;
            Hp = _maxHp > Hp ? Hp : MaxHp;
        }
    }

    public int Hp
    {
        get => _hp;
        set => _hp = value > 0 ? value : 0;
    }

    public int OffensivePower
    {
        get => _offensivePower;
        set => _offensivePower = value > 0 ? value : 0;
    }

    public int DefensivePower
    {
        get => _defensivePower;
        set => _defensivePower = value > 0 ? value : 0;
    }

    public int PiercingDamage
    {
        get => _piercingDamage;
        set => _piercingDamage = value > 0 ? value : 0;
    }

    [SerializeField] private InfoWindow _infoWindow;
    private Animator _animator;
    private int _hp;
    private int _offensivePower;
    private int _defensivePower;
    private int _piercingDamage;

    public InfoWindow InfoWindow
    {
        get { return _infoWindow; }
        set { _infoWindow = value; }
    }

    public void Init()
    {
        MaxHp = 100;
        Hp = MaxHp;
        OffensivePower = 2;
        DefensivePower = 0;
        PiercingDamage = 2;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _infoWindow.UpdateHpBar(Hp, MaxHp);
        _infoWindow.UpdateOffensivePowerText(OffensivePower);
        _infoWindow.UpdateDefensivePowerText(DefensivePower);
        _infoWindow.UpdatePiercingDamageText(PiercingDamage);
    }

    public void AttackEnemy()
    {
        IBattleable enemy = BattleManager.Instance.EnemyBattleable;
        enemy.ToDamage(OffensivePower);
        enemy.ToPiercingDamage(PiercingDamage);

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

        damage = damage >= DefensivePower ? damage - DefensivePower : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Logger.Assert(_infoWindow != null);

        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 관통 데미지 {piercingDamage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        Logger.Assert(_infoWindow != null);

        DefensivePower = defensivePower;
        _infoWindow.UpdateDefensivePowerText(DefensivePower);
        Logger.Log($"플레이어 방어력 {defensivePower}로 설정됨", gameObject);
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Logger.Assert(_infoWindow != null);

        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;

        _infoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp}", gameObject);
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
    
    public void FinishAttackAnimation()
    {
        BattleManager.Instance.FinishAttack = true;
    }

    public void FinishDeathAnimation()
    {
        Destroy(this.gameObject);
        FadeManager.Instance.StartFadeOut();
        StageManager.Instance.OpenStageAddListener(StageManager.Instance.GetNextStage());
    }
}
