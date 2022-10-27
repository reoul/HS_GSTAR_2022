using System.Collections.Generic;
using UnityEngine;

public class Enemy003 : Enemy
{
    public override string EnemyName => "유령 검사";
    public override int MaxHp => 35;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int FixedDamage { get; protected set; }
}
