using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy002 : Enemy
{
    public override string EnemyName => "영혼을 먹는자";
    public override int MaxHp => 40;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int FixedDamage { get; protected set; }
}
