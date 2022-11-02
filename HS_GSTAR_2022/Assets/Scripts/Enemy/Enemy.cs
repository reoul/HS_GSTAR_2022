using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class Enemy : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public string EnemyName { get; }
    public int MaxHp { get; set; }
    public int Hp { get; set; }
    public int OffensivePower { get; set; }
    public int DefensivePower { get; set; }
    public int PiercingDamage { get; set; }
    public InfoWindow InfoWindow { get; set; }

    private Animator _animator;

    private void Awake()
    {
        Hp = MaxHp;
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        InfoWindow.UpdateOffensivePowerText(OffensivePower);
        InfoWindow.UpdateDefensivePowerText(DefensivePower);
        InfoWindow.UpdatePiercingDamageText(PiercingDamage);
    }

    public void AttackPlayer()
    {
        IBattleable player = BattleManager.Instance.PlayerBattleable;
        player.ToDamage(OffensivePower);
        player.ToPiercingDamage(PiercingDamage);

        SoundManager.Instance.PlaySound("MP_AttackSound");

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
        damage = damage >= DefensivePower ? damage - DefensivePower : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 데미지 {damage} 입힘. 현재 체력 : {Hp}", gameObject);
    }

    public void ToPiercingDamage(int piercingDamage)
    {
        Hp = Hp - piercingDamage > 0 ? Hp - piercingDamage : 0;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 관통 데미지 {piercingDamage} 입힘. 현재 체력 : {Hp}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        DefensivePower = defensivePower;
        InfoWindow.UpdateDefensivePowerText(DefensivePower);
        Logger.Log($"적 {name}의 방어력 {defensivePower}로 설정됨", gameObject);
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 {heal} 힐. 현재 체력 : {Hp}", gameObject);
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
        StageManager.Instance.SetFadeEvent(StageManager.Instance.NextStageType);
    }
}
