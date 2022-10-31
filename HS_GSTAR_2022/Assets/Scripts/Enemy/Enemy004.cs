using System.Collections.Generic;
using UnityEngine;

public class Enemy004 : Enemy
{
    public override string EnemyName => "골렘 가디언";
    public override int MaxHp { get; set; }
    public override int OffensivePower { get; set; }
    public override int DefensivePower { get; set; }
    public override int PiercingDamage { get; set; }
}
