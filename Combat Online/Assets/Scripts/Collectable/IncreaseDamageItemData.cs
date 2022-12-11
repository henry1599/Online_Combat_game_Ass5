using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseDamageItemData", menuName = "ScriptableObjects/IncreaseDamageItemData", order = 2)]
public class IncreaseDamageItemData : ScriptableObject
{
    public int Amount;
    public int Time;
}
