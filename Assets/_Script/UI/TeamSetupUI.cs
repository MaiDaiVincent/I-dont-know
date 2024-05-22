using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class BgOutlineColor
{
    public BgColorOutlineType type;
    public Color color;
}
public class TeamSetupUI : MonoBehaviour
{
    [SerializeField] private GameObject interactiveBg;

    [SerializeField] private Button startButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private List<BgOutlineColor> bgOutlineColorList;

    public List<FungusSlot> fungusSlotList = new List<FungusSlot>();
    public List<FungusCard> fungusCardList = new List<FungusCard>();
    private ManagerRoot managerRoot => ManagerRoot.Instance;
    private void Awake()
    {
        startButton.onClick.AddListener(OnClickStart);
        returnButton.onClick.AddListener(OnClickReturn);

        InteractiveBgState(false);

    }
    private void Start()
    {
        EventManager.onUnPickFungus += SortFungiList;
        EventManager.onPickFungus += SortFungiList;

    }
    private void OnDestroy()
    {
        EventManager.onUnPickFungus -= SortFungiList;
        EventManager.onPickFungus -= SortFungiList;
    }
    void InteractiveBgState(bool state) => interactiveBg.SetActive(state);

    private void OnClickReturn()
    {
        managerRoot.TransitionToScene(managerRoot.ManagerRootConfig.home);
    }
    private void OnClickStart()
    {
        bool canChangeScene = true;
        foreach(var slot in fungusSlotList)
        {
            if (slot.FungusPackedConfig == null)
            {
                canChangeScene = false;
            }
        }

        if (canChangeScene)
        {
            InteractiveBgState(transform);

            managerRoot.GetNameTypeFungusPicked(fungusSlotList);
            managerRoot.TransitionToScene(managerRoot.ManagerRootConfig.room);
        }
    }

    public void SortFungiList(int index, FungusPackedConfig config)
    {
        SortFungusCardList();
        SortFungusSlotList();
    }
    public void SortFungusSlotList()
    {
        for (int i = 0; i < fungusSlotList.Count; i++)
        {
            if (fungusSlotList[i].FungusPackedConfig == null)
            {
                for (int j = i + 1; j < fungusSlotList.Count; j++)
                {
                    if (fungusSlotList[j].FungusPackedConfig != null)
                    {
                        fungusSlotList[i].GetFungusPackedConfig(fungusSlotList[j].FungusPackedConfig);
                        fungusSlotList[i].LoadConfig();
                        fungusSlotList[j].ResetConfig();

                        break;
                    }
                }
            }
        }
    }
    public void SortFungusCardList()
    {
        for (int i = 0; i < fungusCardList.Count; i++)
        {
            if (fungusCardList[i].slotIndex != 0)
            {
                for (int j = 0; j < fungusSlotList.Count; j++)
                {
                    if (fungusCardList[i].packedConfig == fungusSlotList[j].FungusPackedConfig)
                    {
                        fungusCardList[i].SetSlotIndex(true, fungusSlotList[j].slotIndex);
                    }
                }
            }
        }
    }
    public Color GetColor(BgColorOutlineType bgColorType)
    {
        Color color = new Color();
        foreach (var bgOutlineColor in bgOutlineColorList)
        {
            if (bgOutlineColor.type == bgColorType)
            {
                color = bgOutlineColor.color;
            }
        }
        return color;
    }
}
