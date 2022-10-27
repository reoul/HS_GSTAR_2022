using System.Collections.Generic;
using UnityEngine;

public class Enemy007 : Enemy
{
    public override string EnemyName => "타우로스";
    public override int MaxHp => 200;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int FixedDamage { get; protected set; }
}
