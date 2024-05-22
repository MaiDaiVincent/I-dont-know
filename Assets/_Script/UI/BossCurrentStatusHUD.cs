using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class BossCurrentStatusHUD : CurrentStatusHUD
{
    private BossInfoReader bossInfo;

    private void Awake()
    {
        EventManager.onSpawnBossInit += OnSpawnBossInit;

    }
    private void OnDestroy()
    {
        EventManager.onSpawnBossInit -= OnSpawnBossInit;
    }
    void OnSpawnBossInit(BossInfoReader info)
    {

        bossInfo = info;
        var bossData = bossInfo.BossData;

        SetHealthSliderInit(0, bossData.maxHealth);
        SetDamageSliderInit(0, bossData.maxHealth);

        SetCurrentHealthSlider(bossData.health);
        SetCurrentDamageSlider(bossData.health);

        SetHealthText(bossData.health, bossData.maxHealth);
        SetLvText(bossData.lv);

        bossInfo.BossController.BossHealth.OnTakeDamageEvent += OnTakeDamage;
        bossInfo.BossData.OnHealthChangeEvent += OnHealthChange;
    }
    protected override void OnTakeDamage(int value)
    {
        base.OnTakeDamage(value);
    }
}
