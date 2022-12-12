using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyManagerConfig", menuName = "ScriptableObjects/EnemyManagerConfig", order = 7)]
public class EnemyManagerConfig : ScriptableObject
{
    public float MinDelayTime;
    public float MaxDelayTime;
    [Range(1, 100)]
    public int EnemySkillRate;
    public int MaxEnemy;
    public float DelayDestroy;
}
