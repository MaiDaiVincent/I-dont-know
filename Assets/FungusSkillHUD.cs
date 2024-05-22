using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class FungusSkillHUD : MonoBehaviour
{
    public Slider cooldownSlider;
    public TextMeshProUGUI cooldownText;
    public Image skillImage;
    public AttackType attackType;
    private FungusInfoReader fungusInfo;
    private FungusAttack fungusAttack;

    private void Awake()
    {
        EventManager.onSwitchFungus += OnSwitchFungus;
        SetCooldownState(false);
    }
    private void OnDestroy()
    {
        EventManager.onSwitchFungus -= OnSwitchFungus;
    }

    private void Update()
    {

    }
    public void SetCooldownSliderInit(float minValue, float maxValue)
    {
        cooldownSlider.minValue = minValue;
        cooldownSlider.maxValue = maxValue;
    }
    public void SetCurrentCooldownSlider(float value) => cooldownSlider.value = value;
    public void SetCooldownText(float value) => cooldownText.text = value.ToString("F1");
    public void SetSkillIcon(Sprite sprite) => skillImage.sprite = sprite;
    public void SetCooldownState(bool state)
    {
        cooldownSlider.gameObject.SetActive(state);
        cooldownText.gameObject.SetActive(state);
    }

    public void SetES_CooldownState(float value, bool state)
    {
        SetCooldownState(state);
        SetCurrentCooldownSlider(value);
        SetCooldownText(value);
    }
    public void SetEB_CooldownState(float value, bool state)
    {
        SetCooldownState(state);
        SetCurrentCooldownSlider(value);
        SetCooldownText(value);
    }

    public void OnSwitchFungus(FungusInfoReader info, FungusCurrentStatusHUD CurrentStatusHUD)
    {
        fungusInfo = info;
        fungusAttack = fungusInfo.FungusController.FungusAttack;

        FungusSkillConfig skillConfig = fungusInfo.FungusData.skillConfig;

        if(attackType == AttackType.ES)
        {
            SkillConfig eS_Skill = skillConfig.eS_SkillConfig;
            SetCooldownSliderInit(0, eS_Skill.cooldown);

            if(fungusAttack.eSTimeIsCooling > 0)
            {
                SetES_CooldownState(fungusAttack.eSTimeIsCooling, true);
            }
            else
            {
                SetES_CooldownState(fungusAttack.eSTimeIsCooling, false);
            }
        }
        else if(attackType == AttackType.EB)
        {
            SkillConfig eB_Skill = skillConfig.eB_SkillConfig;
            SetCooldownSliderInit(0, eB_Skill.cooldown);

            if (fungusAttack.eBTimeIsCooling > 0)
            {
                SetEB_CooldownState(fungusAttack.eBTimeIsCooling, true);
            }
            else
            {
                SetEB_CooldownState(fungusAttack.eBTimeIsCooling, false);
            }
        }
        
    }
}
