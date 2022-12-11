using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ScriptableObjects/ProjectileConfig", order = 6)]
public class ProjectileConfig : ScriptableObject
{
    public float Speed;
    public float DamageFactor;
    public float LifeTime;
}
