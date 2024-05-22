using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static EventManager;
using static UnityEngine.Rendering.DebugUI;

public class CurrentStatusHUD : MonoBehaviour
{

    [SerializeField] private int takingDamage;

    public Slider currentHealthSlider;
    public Slider currentDamageSlider;

    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI lvText;

    protected Coroutine updateCurrentDamageSliderCoroutine;


    #region Get Set Reference
    public void SetHealthSliderInit(float minValue, float maxValue)
    {
        currentHealthSlider.minValue = minValue;
        currentHealthSlider.maxValue = maxValue;
    }
    public void SetDamageSliderInit(float minValue, float maxValue)
    {
        currentDamageSlider.minValue = minValue;
        currentDamageSlider.maxValue = maxValue;
    }
    public void SetCurrentHealthSlider(float value) => currentHealthSlider.value = value;
    public void SetCurrentDamageSlider(float value)
    {
        currentDamageSlider.value = value;
    }
    public void SetHealthText(float value, float maxValue) => healthText.text = value + " / " + maxValue;
    public void SetLvText(int lvValue) => lvText.text = "Lv." + lvValue.ToString();
    #endregion


    protected virtual void OnTakeDamage(int value)
    {

        updateCurrentDamageSliderCoroutine = StartCoroutine(UpdateCurrentDamageSliderCoroutine(value));
    }


    protected virtual void OnHealthChange(float health, float maxHealth)
    {
        SetCurrentHealthSlider(health);
        SetHealthText(health, maxHealth);
    }

    protected virtual IEnumerator UpdateCurrentDamageSliderCoroutine(int value)
    {
        float waitTime;

        if(takingDamage == 0)
        {
            takingDamage = value;
            waitTime = GameConfig.damageSliderChangeWaitTime;
        }
        else
        {
            takingDamage += value;
            waitTime = GameConfig.damageSliderChangeWaitTime /2;
        }

        yield return new WaitForSeconds(waitTime);

        float counter = currentDamageSlider.value;

        while (Mathf.RoundToInt(currentHealthSlider.value) < Mathf.RoundToInt(currentDamageSlider.value))
        {
            counter -= takingDamage * 2 * Time.deltaTime;
            SetCurrentDamageSlider(counter);
            yield return null;
        }

        takingDamage = 0;
    }
}
