using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnArea", menuName = "ScriptableObjects/SpawnArea", order = 4)]
public class SpawnArea : ScriptableObject
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
}
