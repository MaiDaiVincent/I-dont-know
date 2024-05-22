using System;
using UnityEngine;

public class BossHealth : HealthBase
{
    private BossData bossData;

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
        bossData = info.BossData;
    }

    public override void TakeDamage(int value)
    {
        int damage = value;

        bossData.health -= damage;

        if (bossData.health <= 0) bossData.health = 0;


        var takingDamage = damage;

        OnTakeDamageEvent?.Invoke(takingDamage);
        if(bossData.health <= 0)
        {
            bossData.health = 0;
            OnDiedEvent?.Invoke();
        }
    }
 






















}
