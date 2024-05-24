using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Spawn
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
}

[Serializable]
public class Wave
{
    public bool isEndWave = false;
    public List<Spawn> spawnList;
}

public class SpawnerManager : Singleton<SpawnerManager>
{
    public Wave currentWave;

    [SerializeField] private Transform root; //obj player holder

    [SerializeField] private List<Wave> waveList;

    private ManagerRoot managerRoot => ManagerRoot.Instance;
    private EnemyManager enemyManager => EnemyManager.Instance;
    private void Start()
    {
        Init();
    }
    void Init()
    {
        SpawnBossInit();
        //SpawnWave();
        SpawnFungusInit();
    }
    public void SpawnFungusInit()
    {
        if (managerRoot == null) return;

        List<FungusNameType> fungusNameTypeList = managerRoot.actionFungusNameList;
        AvailableFungiConfig availableFungiConfig = managerRoot.ManagerRootConfig.availableFungiConfig;

        List<FungusInfoReader> fungusInfoList = new List<FungusInfoReader>();
        
        foreach (FungusNameType fungusNameType in fungusNameTypeList)
        {
            FungusPackedConfig fungusPackedConfig;
            fungusPackedConfig = availableFungiConfig.GetFungusPackedConfigByNameType(fungusNameType);

            FungusInfoReader fungusInfo = Instantiate(fungusPackedConfig.fungusInfoReader, root);

            FungusData fungusData = new FungusData();

            //stats
            fungusData.atk = fungusPackedConfig.stats.atk;
            fungusData.lv = fungusPackedConfig.stats.lv;
            fungusData.moveSpeed = fungusPackedConfig.stats.moveSpeed;

            //health
            fungusData.maxHealth = fungusPackedConfig.stats.maxHealth;
            fungusData.health = fungusPackedConfig.stats.maxHealth;


            //dash
            fungusData.dashTime = fungusPackedConfig.stats.dashTime;
            fungusData.dashForce = fungusPackedConfig.stats.dashForce;
            fungusData.dashStamina = fungusPackedConfig.stats.dashStamina;

            //crit
            fungusData.critRate = fungusPackedConfig.stats.critRatePercent;
            fungusData.critDamagePercent = fungusPackedConfig.stats.critDamagePercent;
            
            //elementalMastery
            fungusData.elementalMastery = fungusPackedConfig.stats.elementalMastery;

            //reference
            fungusData.fungusConfig = fungusPackedConfig.config;
            fungusData.fungusStats = fungusPackedConfig.stats;
            fungusData.skillConfig = fungusPackedConfig.skillConfig;

            fungusInfo.GetData(fungusData);

            fungusInfoList.Add(fungusInfo);
        }
        
        EventManager.ActionOnSpawnFungusInit(fungusInfoList);
    }

    public void SpawnBossInit()
    {
        if (managerRoot == null) return;

        BossNameType actionBossNameType = managerRoot.actionBossNameType;
        AvailableBossConfig availableBossConfig = managerRoot.ManagerRootConfig.availableBossConfig;

        BossPackedConfig bossPackedConfig;
        bossPackedConfig = availableBossConfig.GetBossPackedConfigByNameType(actionBossNameType);

        BossData bossData = new BossData();

        bossData.bossConfig = bossPackedConfig.config;
        bossData.bossStats = bossPackedConfig.stats;

        bossData.lv = bossData.bossStats.lv;
        bossData.maxHealth = bossData.bossStats.maxHealth;
        bossData.health = bossData.bossStats.maxHealth;
        bossData.damage = bossData.bossStats.damage;
        bossData.moveSpeed = bossData.bossStats.moveSpeed;

        BossInfoReader bossInfo = Instantiate(bossPackedConfig.bossInfoReader);
        bossInfo.GetData(bossData);

        EventManager.ActionOnSpawnBossInit(bossInfo);
    }
    public void SpawnWave()
    {
        if (waveList.Count <= 0 || !WaveEnded()) return;

        if(currentWave != null && currentWave == waveList[waveList.Count - 1])
        {
            SpawnBossInit();
            return;
        }
        foreach (var wave in waveList)
        {
            if (!wave.isEndWave)
            {
                currentWave = wave;
                SpawnEnemy(wave.spawnList);
                break;
            }
        }
    }
    public void SpawnEnemy(List<Spawn> spawnList)
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            var spawn = spawnList[i];
            var enemy = Instantiate(spawn.enemyPrefab, spawn.spawnPoint);
            enemy.transform.SetParent(enemyManager.enemyHolder.transform);
        }
    }
    bool WaveEnded()
    {
        if (enemyManager.enemyHolder.transform.childCount == 0) return true;
        return false;
    }

    void CheckWave()
    {
        if (WaveEnded())
        {
            SpawnWave();
        }
    }
}
