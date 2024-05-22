using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FungusSlotHUD : MonoBehaviour
{
    public bool CanActive => fungusData.health != 0;

    public FungusData fungusData;
    [SerializeField] private int takingDamage;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider slotInteractRecoveryBar;

    [SerializeField] private TextMeshProUGUI inputSlotText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI slotInteractRecoveryText;
    [SerializeField] private Image avatarImage;

    [SerializeField] private GameObject slotInteractRecoveryObj;

    private Image choseBgImage;
    private CanvasGroup canvasGroup;

    private Coroutine updateDamageSlotSliderCoroutine;

    private void Awake()
    {
        choseBgImage = GetComponentInChildren<Image>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }
    private void Start()
    {
        SetSlotInteractRecoverySliderInit(0, GameConfig.switchSlotRecoveryTime);
    }

    public void OnTakeDamage(int value)
    {
        this.takingDamage = value;
        if (updateDamageSlotSliderCoroutine != null) StopCoroutine(updateDamageSlotSliderCoroutine);
        updateDamageSlotSliderCoroutine = StartCoroutine(UpdateDamageSlotSliderCoroutine());
    }

    public void OnHealthChange(float health, float maxHealth)
    {
        SetHealthSlider(health);
    }


    IEnumerator UpdateDamageSlotSliderCoroutine()
    {
        float healthValue = healthSlider.value;
        float damageValue = damageSlider.value;

        yield return new WaitForSeconds(GameConfig.damageSliderChangeWaitTime);

        float counter = damageValue;
        while (Mathf.RoundToInt(healthValue) < Mathf.RoundToInt(damageValue))
        {
            counter -= takingDamage * 2 * Time.deltaTime;
            SetDamageSlider(counter);
            yield return null;
        }
    }


    #region Get Set Reference
    public void SetHealthSliderInit(float minValue, float maxValue)
    {
        healthSlider.minValue = minValue;
        healthSlider.maxValue = maxValue;
    }
    public void SetDamageSliderInit(float minValue, float maxValue)
    {
        damageSlider.minValue = minValue;
        damageSlider.maxValue = maxValue;
    }
    public void SetSlotInteractRecoverySliderInit(float minValue, float maxValue)
    {
        slotInteractRecoveryBar.minValue = minValue;
        slotInteractRecoveryBar.maxValue = maxValue;
    }
    public void SetHealthSlider(float value) => healthSlider.value = value;

    public void SetSlotInteractRecoverySlider(float value) => slotInteractRecoveryBar.value = value;

    public void SetDamageSlider(float value) => damageSlider.value = value;
    
    public void SetName(string name) => nameText.text = name;

    public void SetFade(float value) => canvasGroup.alpha = value;

    public void SetSlotInteractRecoveryState(bool state) => slotInteractRecoveryObj.SetActive(state);

    public void SetAvatar(Sprite sprite) => avatarImage.sprite = sprite;
    
    public void SetInputSlot(KeyCode keyCode)
    {
        inputSlotText.text = keyCode.ToString().Substring(keyCode.ToString().Length - 1);
    }
    public void SetChoseBgColor(Color color, float alpha)
    {
        Color newColor = color;
        newColor.a = alpha;
        choseBgImage.color = newColor;
    }

    public void SetSlotInteractRecoveryTime(float value)
    {
        slotInteractRecoveryText.text = value.ToString("F1");
    }
    #endregion

    public Color ChoseBgColor() => choseBgImage.color;



}
