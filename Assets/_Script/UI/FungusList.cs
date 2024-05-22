using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusList : MonoBehaviour
{
    public FungusCard fungusCardPrefab;
    private TeamSetupUI teamSetupUI;

    private ManagerRoot managerRoot => ManagerRoot.Instance;
    private void Awake()
    {
        teamSetupUI = transform.root.GetComponent<TeamSetupUI>();
    }
    private void Start()
    {
        LoadFungusList();
    }
    void LoadFungusList()
    {
        List<FungusPackedConfig> fungusPackedConfigList = 
            managerRoot.ManagerRootConfig.availableFungiConfig.fungusPackedConfigList;

        foreach(var fungusPackedConfig in fungusPackedConfigList)
        {
            FungusCard fungusCard = Instantiate(fungusCardPrefab, transform);
            fungusCard.packedConfig = fungusPackedConfig;
            fungusCard.LoadConfig();

            teamSetupUI.fungusCardList.Add(fungusCard);
        }
    }
}
