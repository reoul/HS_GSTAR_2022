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
    public abstract int FixedDamage { get; protected set; }

    [SerializeField]
    private TMP_Text _healthText, _shieldText;

    private void Awake()
    {
        name = EnemyName;
        foreach (TMP_Text text in transform.GetComponentsInChildren<TMP_Text>())
        {
            if (text.name == "NameText")
            {
                text.text = EnemyName;
                break;
            }
        }
        Hp = MaxHp;
    }

    private void Start()
    {
        UpdateInfo();
    }

    public void ToDamage(int damage)
    {
        damage = damage >= DefensivePower ? damage - DefensivePower : 0;
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        
        UpdateInfo();
        Logger.Log($"적 {name}에게 데미지 {damage} 입힘. 현재 체력 : {Hp}", gameObject);
    }

    public void ToFixedDamage(int fixedDamage)
    {
        Hp = Hp - fixedDamage > 0 ? Hp - fixedDamage : 0;
        
        UpdateInfo();
        Logger.Log($"적 {name}에게 고정 데미지 {fixedDamage} 입힘. 현재 체력 : {Hp}", gameObject);
    }

    public void SetDefensivePower(int defensivePower)
    {
        DefensivePower = defensivePower;
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        UpdateInfo();
        Logger.Log($"적 {name}에게 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }

    private void UpdateInfo()
    {
        _healthText.text = Hp.ToString();
    }
}
