using System.Collections.Generic;
using UnityEngine;

public class Enemy006 : Enemy
{
    public override string EnemyName => "캡푸";
    public override int MaxHp => 125;
    public override int OffensivePower { get; protected set; }
    public override int DefensivePower { get; protected set; }
    public override int PiercingDamage { get; protected set; }
}
