using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy002 : Enemy
{
    public override string EnemyName => "영혼을 먹는자";
    public override int MaxHp { get; set; }
    public override int OffensivePower { get; set; }
    public override int DefensivePower { get; set; }
    public override int PiercingDamage { get; set; }
}
