using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public sealed class Player : MonoBehaviour, IBattleable
{
    public GameObject OwnerObj => this.gameObject;
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int OffensivePower { get; private set; }
    public int DefensivePower { get; private set; }
    public int FixedDamage { get; private set; }
    private List<string> _cardDeck;

    [SerializeField]
    private TMP_Text _healthText, _shieldText;

    private void Awake()
    {
        MaxHp = 100;
        Hp = MaxHp;
        OffensivePower = 2;
        DefensivePower = 0;
        FixedDamage = 2;
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
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void ToFixedDamage(int fixedDamage)
    {
        Hp = Hp - fixedDamage > 0 ? Hp - fixedDamage : 0;
        
        UpdateInfo();
        Logger.Log($"플레이어 고정 데미지 {fixedDamage} 입음. 현재 체력 {Hp}", gameObject);
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
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }

    private void UpdateInfo()
    {
        _healthText.text = Hp.ToString();
    }
}
