using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class Enemy : Player
{
    protected override bool IsPlayer => false;
}
