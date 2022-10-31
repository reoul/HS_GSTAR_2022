using System;
using System.Collections.Generic;
using UnityEngine;

public enum ECrowdControl
{
    Poison
}

public interface IBattleable
{
    GameObject OwnerObj { get; }

    /// <summary> 최대 체력 </summary>
    int MaxHp { get; set; }

    /// <summary> 체력 </summary>
    int Hp { get; set; }

    /// <summary> 공격력 </summary>
    int OffensivePower { get; set; }

    /// <summary> 방어력 </summary>
    int DefensivePower { get; set; }

    /// <summary> 관통 데미지 </summary>
    int PiercingDamage { get; set; }

    /// <summary> 정보창 </summary>
    public InfoWindow InfoWindow { get; set; }

    /// <summary> 데미지를 준다 </summary>
    /// <param name="damage">데미지</param>
    void ToDamage(int damage);

    /// <summary> 관통 데미지를 준다 </summary>
    /// <param name="piercingDamage">관통 데미지</param>
    void ToPiercingDamage(int piercingDamage);

    /// <summary> 방어력 설정 </summary>
    void SetDefensivePower(int defensivePower);

    /// <summary> 상태이상을 준다 </summary>
    /// <param name="cc">상태이상 타입</param>
    /// <param name="coefficient">상태이상 계수</param>
    void ToCC(ECrowdControl cc, int coefficient);

    /// <summary> 체력을 회복한다 </summary>
    /// <param name="heal">힐</param>
    void ToHeal(int heal);

    /// <summary> Attack 애니메이션 시작 </summary>
    void StartAttackAnimation();

    /// <summary> Hit 애니메이션 시작 </summary>
    void StartHitAnimation();

    /// <summary> Dead 애니메이션 시작 </summary>
    void StartDeadAnimation();
}
