using System.Collections.Generic;
using UnityEngine;

public class Enemy008 : Enemy
{
    public override string EnemyName => "유령기사";
    public override int MaxHp { get; set; }
    public override int OffensivePower { get; set; }
    public override int DefensivePower { get; set; }
    public override int PiercingDamage { get; set; }
}
