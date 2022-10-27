using System.Collections.Generic;
using UnityEngine;

public class Enemy008 : Enemy
{
    public override string EnemyName => "유령기사";
    public override int MaxHp => 300;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int FixedDamage { get; protected set; }
}
