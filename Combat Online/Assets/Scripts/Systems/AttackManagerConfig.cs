using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackManagerConfig", menuName = "ScriptableObjects/AttackManagerConfig", order = 5)]
public class AttackManagerConfig : ScriptableObject
{
    public float MaxDistance;
    public float MaxAngle;
}
