using System.Collections.Generic;
using UnityEngine;

public class Enemy005 : Enemy
{
    public override string EnemyName => "카브라스";
    public override int MaxHp => 25;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int FixedDamage { get; protected set; }
}
