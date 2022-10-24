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
    public abstract int StartShield { get; }
    public int Shield { get; protected set; }

    [SerializeField]
    private TMP_Text _healthText, _shieldText;

    private void Awake()
    {
        name = EnemyName;
        Hp = MaxHp;
        Shield = StartShield;
    }

    private void Start()
    {
        UpdateInfo();
    }

    public void ToDamage(int damage)
    {
        if (damage >= Shield)
        {
            damage -= Shield;
            Shield = 0;
        }
        else
        {
            Shield -= damage;
            damage = 0;
        }

        Hp = Hp - damage > 0 ? Hp - damage : 0;
        UpdateInfo();
        Logger.Log($"적 {name}에게 데미지 {damage} 입힘. 현재 체력 : {Hp}", gameObject);
    }

    public void SetShield(int shield)
    {
        Shield = shield;
        UpdateInfo();
        Logger.Log($"적 {name}에게 실드 {Shield} 설정", gameObject);
    }

    public void ToShield(int shield)
    {
        Shield += shield;
        UpdateInfo();
        Logger.Log($"적 {name}에게 실드 {shield} 증가. 현재 실드량 : {Shield}", gameObject);
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

    /// <summary>
    /// 가지고 있는 카드들 코드를 가져옴
    /// </summary>
    /// <returns>가지고 있는 카드들 코드</returns>
    protected abstract List<string> GetCharacterCardCodes();

    public List<string> GetCardCodes()
    {
        return GetCharacterCardCodes();
    }

    private void UpdateInfo()
    {
        _healthText.text = Hp.ToString();
        _shieldText.text = Shield.ToString();
    }
}
