using System.Collections.Generic;
using UnityEngine;

public class Enemy004 : Enemy
{
    public override string EnemyName => "골렘 가디언";
    public override int MaxHp => 80;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int PiercingDamage { get; protected set; }
}
