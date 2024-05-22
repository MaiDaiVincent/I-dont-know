using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FungusCurrentStatusHUD : CurrentStatusHUD
{
    public FungusInfoReader fungusInfo;

    private void Awake()
    {
        EventManager.onSwitchFungus += OnSwitchFungus;
    }
    private void OnDestroy()
    {
        EventManager.onSwitchFungus -= OnSwitchFungus;
    }

    void OnSwitchFungus(FungusInfoReader info, FungusCurrentStatusHUD CurrentStatusHUD)
    {
        if (updateCurrentDamageSliderCoroutine != null) StopCoroutine(updateCurrentDamageSliderCoroutine);

        fungusInfo = info;
        FungusData fungusData = fungusInfo.FungusData;

        SetHealthSliderInit(0, fungusData.maxHealth);
        SetDamageSliderInit(0, fungusData.maxHealth);

        SetCurrentHealthSlider(fungusData.health);
        SetCurrentDamageSlider(fungusData.health);

        SetHealthText(fungusData.health, fungusData.maxHealth);
        SetLvText(fungusData.lv);

        fungusInfo.FungusController.FungusHealth.OnTakeDamageEvent += OnTakeDamage;
        fungusInfo.FungusData.OnHealthChangeEvent += OnHealthChange;

    }

}
