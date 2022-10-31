using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Enemy : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public abstract string EnemyName { get; }
    public abstract int MaxHp { get; }
    public int Hp { get; protected set; }
    public abstract int OffensivePower { get; protected set; }
    public abstract int DefensivePower { get; protected set; }
    public abstract int PiercingDamage { get; protected set; }
    public InfoWindow InfoWindow { get; set; }

    private void Awake()
    {
        Hp = MaxHp;
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
        BattleManager.Instance.PlayerBattleable.ToDamage(OffensivePower);
        BattleManager.Instance.PlayerBattleable.ToPiercingDamage(PiercingDamage);
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

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        
        InfoWindow.UpdateHpBar(Hp, MaxHp);
        Logger.Log($"적 {name}에게 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }
}
