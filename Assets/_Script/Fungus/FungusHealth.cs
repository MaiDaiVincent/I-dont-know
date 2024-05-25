using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusHealth : HealthBase
{
    public FungusData fungusData { get; private set; }
    private void Awake()
    {
        EventManager.onSwitchFungus += OnSwitchFungus;
    }
    private void OnDestroy()
    {
        EventManager.onSwitchFungus -= OnSwitchFungus;
    }

    void OnSwitchFungus(FungusInfoReader fungusInfo, FungusCurrentStatusHUD fungusCurrentStatusHUD)
    {
        fungusData = fungusInfo.FungusData; 

    }
    public override void TakeDamage(int value)
    {
        if (FungusManager.Instance.isHaveShield) return;

        int damage = value;

        fungusData.health -= damage;

        var takingDamage = damage;

        OnTakeDamageEvent?.Invoke(takingDamage);

        if (fungusData.health <= 0)
        {
            fungusData.health = 0;
            OnDiedEvent?.Invoke();
        }
    }

}
