using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Player : MonoBehaviour, IBattleable
{
    public int MaxHp { get; private set; }
    public int Hp { get; private set; }
    public int Shield { get; private set; }

    private void Awake()
    {
        MaxHp = 100;
        Hp = MaxHp;
        Shield = 0;
    }

    public void ToDamage(int damage)
    {
        Hp = Hp - damage > 0 ? Hp - damage : 0;
        Logger.Log($"플레이어 데미지 {damage} 입음. 현재 체력 {Hp}", gameObject);
    }

    public void SetShield(int shield)
    {
        Shield = shield;
        Logger.Log($"플레이어 실드 {Shield} 설정", gameObject);
    }

    public void ToShield(int shield)
    {
        Shield += shield;
        Logger.Log($"플레이어 실드 {shield} 증가. 현재 실드량 : {Shield}", gameObject);
    }

    public void ToCC(ECrowdControl cc, int coefficient)
    {
        throw new System.NotImplementedException();
    }

    public void ToHeal(int heal)
    {
        Hp = Hp + heal < MaxHp ? Hp + heal : MaxHp;
        Logger.Log($"플레이어 {heal} 힐. 현재 체력 : {Hp}", gameObject);
    }

    public List<string> GetCardCodes()
    {
        return new List<string> {"001", "003"};
    }
}
