using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossData
{
    public float maxHealth;
    public float health
    {
        get => _health;
        set
        {
            if (value != _health)
            {
                _health = Mathf.Clamp(value, 0, maxHealth);
                OnHealthChangeEvent?.Invoke(health, maxHealth);
            }
        }
    }
    private float _health;

    public int damage;

    public int lv;

    public float moveSpeed;

    public BossConfig bossConfig;
    public BossStats bossStats;

    public Action<float, float> OnHealthChangeEvent;

}
