using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FungusSlotListHUD : MonoBehaviour
{
    [SerializeField] private FungusSlotHUD fungusSlotHUDPrefab;
    public List<FungusSlotHUD> FungusSlotHUDList { get => fungusSlotHUDList; }
    [SerializeField] private List<FungusSlotHUD> fungusSlotHUDList = new List<FungusSlotHUD>();

    public void SetInit(List<FungusInfoReader> fungusInfoList, List<KeyCode> inputSlot)
    {
        for (int i = 0; i < fungusInfoList.Count; i++)
        {
            FungusSlotHUD fungusSlotHUD = Instantiate(fungusSlotHUDPrefab, transform);
            FungusData fungusData = fungusSlotHUD.fungusData = fungusInfoList[i].FungusData;

            fungusSlotHUD.SetHealthSliderInit(0, fungusData.fungusStats.maxHealth);
            fungusSlotHUD.SetDamageSliderInit(0, fungusData.fungusStats.maxHealth);
            fungusSlotHUD.SetHealthSlider(fungusData.fungusStats.maxHealth);
            fungusSlotHUD.SetDamageSlider(fungusData.fungusStats.maxHealth);
            fungusSlotHUD.SetAvatar(fungusData.fungusConfig.fungusAvatar);
            fungusSlotHUD.SetName(fungusData.fungusConfig.fungusName);

            fungusSlotHUD.SetInputSlot(inputSlot[i]);
            fungusSlotHUD.SetChoseBgColor(fungusData.fungusConfig.fungusColor, GameConfig.unChosenSlotAlpha);

            fungusSlotHUDList.Add(fungusSlotHUD);

            //Add Event
            fungusInfoList[i].FungusController.FungusHealth.OnTakeDamageEvent += fungusSlotHUD.OnTakeDamage;
            fungusInfoList[i].FungusData.OnHealthChangeEvent += fungusSlotHUD.OnHealthChange;
        }
    }
    public void SetSlotSelect(int index)
    {
        for (int i = 0; i < fungusSlotHUDList.Count; i++)
        {
            var fungusSlot = fungusSlotHUDList[i];
            Color color = fungusSlot.ChoseBgColor();

            if (i == index) fungusSlot.SetChoseBgColor(color, GameConfig.choseSlotAlpha);
            else fungusSlot.SetChoseBgColor(color, GameConfig.unChosenSlotAlpha);
        }
    }

}
