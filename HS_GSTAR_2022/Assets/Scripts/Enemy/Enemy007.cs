using System.Collections.Generic;
using UnityEngine;

public class Enemy007 : Enemy
{
    public override string EnemyName => "타우로스";
    public override int MaxHp { get; set; }
    public override int OffensivePower { get; set; }
    public override int DefensivePower { get; set; }
    public override int PiercingDamage { get; set; }
}
