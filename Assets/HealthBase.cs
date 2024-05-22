using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public Action<int> OnTakeDamageEvent;
    public Action OnDiedEvent;
    protected PoolManager poolManager => PoolManager.Instance;
    public virtual void TakeDamage(int value) { }
}
