using System.Collections.Generic;
using UnityEngine;

public enum ECrowdControl
{
    Poison
}

public interface IBattleable : IUseCard
{
    GameObject OwnerObj { get; }
    int MaxHp { get; }
    int Hp { get; }
    int Shield { get; }
    
    /// <summary> 데미지를 준다 </summary>
    /// <param name="damage">데미지</param>
    void ToDamage(int damage);
    
    /// <summary> 실드를 설정한다 </summary>
    /// <param name="shield">실드</param>
    void SetShield(int shield);

    /// <summary> 실드를 준다 </summary>
    /// <param name="shield">실드</param>
    void ToShield(int shield);

    /// <summary> 상태이상을 준다 </summary>
    /// <param name="cc">상태이상 타입</param>
    /// <param name="coefficient">상태이상 계수</param>
    void ToCC(ECrowdControl cc, int coefficient);

    /// <summary> 체력을 회복한다 </summary>
    /// <param name="heal">힐</param>
    void ToHeal(int heal);
}
