using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FungusPackedConfig
{
    public FungusNameType fungusNameType;
    public FungusConfig config;
    public FungusStats stats;
    public FungusSkillConfig skillConfig;
    public FungusInfoReader fungusInfoReader;
}
[CreateAssetMenu(fileName = "New Available Fungi Config", menuName = "Config/Available Fungi Config")]
public class AvailableFungiConfig : ScriptableObject
{
    public List<FungusPackedConfig> fungusPackedConfigList;

    private Dictionary<FungusNameType, FungusPackedConfig> fungusConfigDictionary;

    public FungusPackedConfig GetFungusPackedConfigByNameType(FungusNameType fungusName)
    {
        if (fungusConfigDictionary == null)
        {
            fungusConfigDictionary = new Dictionary<FungusNameType, FungusPackedConfig>();
            foreach (FungusPackedConfig config in fungusPackedConfigList)
            {
                fungusConfigDictionary.Add(config.fungusNameType, config);
            }
        }

        if (fungusConfigDictionary.ContainsKey(fungusName))
            return fungusConfigDictionary[fungusName];
        return null;
    }
}
