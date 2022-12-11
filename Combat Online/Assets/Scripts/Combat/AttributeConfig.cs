using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeConfig", menuName = "ScriptableObjects/AttributeConfig", order = 1)]
public class AttributeConfig : ScriptableObject
{
    public int BaseHealth;
    public int MaxHealth;
    public int BaseDamage;
}
