using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IBeHit
{
    public const float StunTime = 0.5f;
    public abstract void BeHit(int damage);
}
