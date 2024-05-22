using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

[System.Serializable]
public class SpawnBossInfo
{
    public BossNameType bossNameType;
    public BossInfoReader bossInfoReader;
}
public class BossManager : Singleton<BossManager>
{
    [SerializeField] private BossInfoReader currentBossInfo;

    [SerializeField] private BossCurrentStatusHUD currentStatusHUD;

    public List<Transform> spawnPointList = new List<Transform>();
    private ManagerRoot managerRoot => ManagerRoot.Instance;

    protected override void Awake()
    {
        EventManager.onFungusDie += OnFungusDie;
        EventManager.onSpawnBossInit += OnSpawnBossInit;

    }
    private void OnDestroy()
    {
        EventManager.onFungusDie -= OnFungusDie;
        EventManager.onSpawnBossInit -= OnSpawnBossInit;
    }
    void OnSpawnBossInit(BossInfoReader bossInfo)
    {
        currentBossInfo = bossInfo;
        currentBossInfo.transform.SetParent(transform);
        currentBossInfo.GetCurrentStatusHUD(currentStatusHUD);
    }
    void OnFungusDie()
    {
    }
}
