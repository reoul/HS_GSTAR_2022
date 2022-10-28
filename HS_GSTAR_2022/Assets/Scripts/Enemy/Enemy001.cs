using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy001 : Enemy
{
    public override string EnemyName => "사형의 집행자";
    public override int MaxHp => 30;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int PiercingDamage { get; protected set; }
}
