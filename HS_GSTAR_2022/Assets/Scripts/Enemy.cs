using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IBattleable
{
    public abstract int MaxHp { get; }
    public int Hp { get; protected set; }
    public int Shield { get; protected set; }

    private void Awake()
    {
        Hp = MaxHp;
        Shield = 0;
    }

    public void ToDamage(int damage)
    {
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        Logger.Log($"적 {name}에게 데미지 {damage} 입힘. 현재 체력 : {Hp}", gameObject);
    }

    public void SetShield(int shield)
    {
        Shield = shield;
        Logger.Log($"적 {name}에게 실드 {Shield} 설정", gameObject);
    }

    public void ToShield(int shield)
    {
        Shield += shield;
        Logger.Log($"적 {name}에게 실드 {shield} 증가. 현재 실드량 : {Shield}", gameObject);
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
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
}
