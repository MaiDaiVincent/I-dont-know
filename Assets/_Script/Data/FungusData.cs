using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FungusData
{
    public float maxHealth;
    public float health
    {
        get => _health;
        set
        {
            if(value != _health)
            {
                _health = Mathf.Clamp(value, 0, maxHealth);
                OnHealthChangeEvent?.Invoke(health, maxHealth);
            }
        }
    }
    private float _health; 
    
    public int atk;
    public int lv;

    public float critRate = 5f;
    public float critDamagePercent = 50f;
    public int elementalMastery = 0;

    public float moveSpeed;

    public float dashTime;
    public float dashForce;
    public int dashStamina;

    public FungusConfig fungusConfig;
    public FungusStats fungusStats;

    public FungusSkillConfig skillConfig;


    public Action<float, float> OnHealthChangeEvent;
}
