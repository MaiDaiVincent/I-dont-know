using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FungusCard : MonoBehaviour
{
    public int slotIndex = 0;
    [SerializeField] private Image avatarImage;
    [SerializeField] private Image elementalImage;
    [SerializeField] private Image bgOutlineImage;
    [SerializeField] private Image slotIndexImage;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI slotIndexText;

    [SerializeField] private Button pickButton;


    public FungusPackedConfig packedConfig;
    private TeamSetupUI teamSetupUI;
    void Awake()
    {
        teamSetupUI = transform.root.GetComponent<TeamSetupUI>();


        SetSlotIndexBgState(false);

        pickButton.onClick.AddListener(() => EventManager.ActionOnPickFungus(GetSlotIndex() , packedConfig));
        EventManager.onPickFungus += OnPickFungus;
        EventManager.onUnPickFungus += OnUnPickFungus;
    }

    private void OnUnPickFungus(int index, FungusPackedConfig config)
    {
        if (packedConfig != config) return;

        SetSlotIndex(false, 0);
        SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.normal));

        foreach (var fungusSlot in teamSetupUI.fungusSlotList)
        {
            if(fungusSlot.FungusPackedConfig == config)
            {
                fungusSlot.OnUnPickFungus(index, config);
            }
        }
        teamSetupUI.SortFungiList(index, config);
    }

    private void OnDestroy()
    {
        EventManager.onPickFungus -= OnPickFungus;

    }
    int GetSlotIndex()
    {
        if(slotIndex == 0)
        {
            for (int i = 0; i < teamSetupUI.fungusSlotList.Count; i++)
            {
                if (teamSetupUI.fungusSlotList[i].FungusPackedConfig == null)
                {
                    return i + 1;
                }
            }
        }
        return 0;
    }
    private void OnPickFungus(int index, FungusPackedConfig config)
    {
        if (packedConfig != config) return;

        if(index == 0)
        {
            OnUnPickFungus(index, config);
            return;
        }
        SetSlotIndex(true, index);
    }

    public void LoadConfig()
    {
        if (packedConfig == null) return;

        SetAvatar(packedConfig.config.fungusAvatar);
        SetElemental(packedConfig.config.fungusElemental.elementalSprite);

        SetLevel(packedConfig.stats.lv);
    }
    void SetAvatar(Sprite avatar) => avatarImage.sprite = avatar;
    void SetLevel(int level) => levelText.text = "Lv. " + level;
    void SetBgOutlineColor(Color color) => bgOutlineImage.color = color;
    void SetElemental(Sprite sprite) => elementalImage.sprite = sprite;
    void SetSlotIndexBgColor(Color color) => slotIndexImage.color = color;
    void SetSlotIndexBgState(bool state) => slotIndexImage.gameObject.SetActive(state);
    public void OnPointerExit()
    {
        transform.DOScale(1f, GameConfig.scaleCardDuration);
        if (slotIndex == 0)
        {
            SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.normal));
        }
        else
        {
            SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.selected));
        }
    }

    public void OnPointerEnter()
    {
        transform.DOScale(1.05f, GameConfig.scaleCardDuration);
        if (slotIndex == 0)
        {
            SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.highlighted));

        }
        else
        {
            SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.selected));

        }
    }

    public void OnPointerDown()
    {
        transform.DOScale(0.95f, GameConfig.scaleCardDuration);
    }
    public void OnPointerClick()
    {
        transform.DOScale(1f, GameConfig.scaleCardDuration);
        if(slotIndex == 0)
        {
            SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.normal));
        }
        else
        {
            SetBgOutlineColor(teamSetupUI.GetColor(BgColorOutlineType.selected));
            SetSlotIndexBgColor(teamSetupUI.GetColor(BgColorOutlineType.selected));
        }
    }

    public void SetSlotIndex(bool state, int index)
    {
        slotIndex = index;

        if (state)
        {
            slotIndexImage.gameObject.SetActive(state);
            slotIndexText.text = slotIndex.ToString();
        }
        else
        {
            slotIndexImage.gameObject.SetActive(state);
            slotIndexText.text = string.Empty;
        }

    }
}
