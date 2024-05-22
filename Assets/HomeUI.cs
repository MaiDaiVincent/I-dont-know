using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private GameObject interactiveBg;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button teamSetupButton;
    [SerializeField] private BossList bossList;

    [SerializeField] private List<BgOutlineColor> bgOutlineColorList;
    private ManagerRoot managerRoot => ManagerRoot.Instance;
    private void Awake()
    {
        returnButton.onClick.AddListener(OnReturnClick);
        teamSetupButton.onClick.AddListener(OnTeamSetupClick);

        InteractiveBgState(false);
    }

    private void OnTeamSetupClick()
    {
        bool canChangeScene = false;
        foreach(var bossSlot in bossList.bossSlotList)
        {
            if (bossSlot.isSelecting) canChangeScene = true;
        }

        if (canChangeScene)
        {
            InteractiveBgState(true);

            managerRoot.GetNameTypeBossChose(bossList.bossSlotList);
            managerRoot.TransitionToScene(managerRoot.ManagerRootConfig.teamSetup);
        }
    }
    void InteractiveBgState(bool state) => interactiveBg.SetActive(state);
    private void OnReturnClick()
    {
        managerRoot.TransitionToScene(managerRoot.ManagerRootConfig.mainMenu);

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
