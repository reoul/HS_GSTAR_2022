using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public sealed class Enemy : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public int MaxHp { get; set; }
    public int Hp { get; set; }
    public Status OffensivePower { get; set; }
    public Status DefensivePower { get; set; }
    public Status PiercingDamage { get; set; }
    
    public InfoWindow InfoWindow { get; set; }
    
    public UnityEvent FinishAttackEvent { get; set; }

    private Animator _animator;

    private void Awake()
    {
        OffensivePower = new Status();
        DefensivePower = new Status();
        PiercingDamage = new Status();

        FinishAttackEvent = new UnityEvent();
        
        Hp = MaxHp;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        InfoWindow.UpdateOffensivePowerText(OffensivePower.FinalStatus);
        InfoWindow.UpdateDefensivePowerText(DefensivePower.FinalStatus);
        InfoWindow.UpdatePiercingDamageText(PiercingDamage.FinalStatus);
    }

    public void AttackPlayer()
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;
        player.ToDamage(OffensivePower.FinalStatus);
        player.ToPiercingDamage(PiercingDamage.FinalStatus);

        SoundManager.Instance.PlaySound("AttackSound");

        if (player.Hp != 0)
        {
            player.StartHitAnimation();
        }
        else
        {
            player.StartDeadAnimation();
        }
    }

    public void ToDamage(int damage)
    {
        damage = damage >= DefensivePower.FinalStatus ? damage - DefensivePower.FinalStatus : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 데미지 {damage} 입힘. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 관통 데미지 {piercingDamage} 입힘. 현재 체력 : {Hp.ToString()}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        DefensivePower.DefaultStatus = defensivePower;
        InfoWindow.UpdateDefensivePowerText(DefensivePower.FinalStatus);
        Logger.Log($"적 {name}의 방어력 {DefensivePower.FinalStatus.ToString()}로 설정됨", gameObject);
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 {heal} 힐. 현재 체력 : {Hp.ToString()}", gameObject);
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
        // 공격 후 이벤트 발동
        FinishAttackEvent.Invoke();
        
        BattleManager.Instance.FinishAttack = true;
    }

    public void FinishDeathAnimation()
    {
        Destroy(this.gameObject);
        FindObjectOfType<BattleStage>().IsFinishBattle = true;
        FindObjectOfType<BattleStage>().IsPlayerWin = true;
        StageManager.Instance.NextStage();
    }
}
