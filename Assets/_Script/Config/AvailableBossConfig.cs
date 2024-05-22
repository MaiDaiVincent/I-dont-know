using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossPackedConfig
{
    public BossNameType bossNameType;
    public BossConfig config;
    public BossStats stats;
    public BossInfoReader bossInfoReader;
}
[CreateAssetMenu(fileName = "New Available Boss Config", menuName = "Config/Available Boss Config")]
public class AvailableBossConfig : ScriptableObject
{
    public List<BossPackedConfig> bossPackedConfigList;

    private Dictionary<BossNameType, BossPackedConfig> bossConfigDictionary;

    public BossPackedConfig GetBossPackedConfigByNameType(BossNameType bossName)
    {
        if (bossConfigDictionary == null)
        {
            bossConfigDictionary = new Dictionary<BossNameType, BossPackedConfig>();
            foreach (BossPackedConfig config in bossPackedConfigList)
            {
                bossConfigDictionary.Add(config.bossNameType, config);
            }
        }

        if (bossConfigDictionary.ContainsKey(bossName))
            return bossConfigDictionary[bossName];
        return null;
    }
}
