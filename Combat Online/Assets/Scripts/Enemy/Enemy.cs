using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Enemy : Player
{
    public override bool IsPlayer => false;

    public override void LogBeHit(int damage)
    {
        Debug.Log("Enemy take damage: " + damage);
    }
}
