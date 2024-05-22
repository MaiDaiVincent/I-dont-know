using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossList : MonoBehaviour
{
    [SerializeField] private BossSlot bossSlotPrefab;
    public List<BossSlot> bossSlotList = new List<BossSlot>();

    private ManagerRoot managerRoot => ManagerRoot.Instance;

    private void Awake()
    {
        EventManager.onShowInfoBoss += OnShowInfoBoss;
        EventManager.onSelectBoss += OnSelectBoss;
    }
    private void Start()
    {
        Init();
    }
    private void OnDestroy()
    {
        EventManager.onShowInfoBoss -= OnShowInfoBoss;
        EventManager.onSelectBoss -= OnSelectBoss;
    }
    private void OnShowInfoBoss(BossSlot bossSlot, bool state)
    {
        foreach (var slot in bossSlotList)
        {
            if(slot != bossSlot)
            {
                slot.SetShowingInfoState(!state);
            }
            else
            {
                slot.SetShowingInfoState(state);
            }
        }
    }
    private void OnSelectBoss(BossSlot bossSlot)
    {
        foreach (var slot in bossSlotList)
        {
            if (slot != bossSlot)
            {
                slot.SetSelectState(false);
                slot.SetShowingInfoState(false);
            }
            else
            {
                slot.SetSelectState(true);
            }
        }
    }
    void Init()
    {
        List<BossPackedConfig> bossPackedConfigList =
            managerRoot.ManagerRootConfig.availableBossConfig.bossPackedConfigList;

        for (int i = 0; i < bossPackedConfigList.Count; i++)
        {
            BossPackedConfig bossPackedConfig = bossPackedConfigList[i];
            BossSlot bossSlot = Instantiate(bossSlotPrefab, transform);
            bossSlot.SetBossPackedConfig(bossPackedConfig);

            bossSlot.SetElementalImage(bossPackedConfig.config.fungusElemental.elementalSprite);
            bossSlot.SetAvatarImage(bossPackedConfig.config.bossAvatar);
            bossSlot.SetName(bossPackedConfig.config.bossName);
            bossSlot.SetHealth(bossPackedConfig.stats.maxHealth);
            bossSlot.SetLv(bossPackedConfig.stats.lv);

            bossSlotList.Add(bossSlot);
        }

    }
}
