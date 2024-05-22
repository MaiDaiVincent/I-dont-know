using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FungusStamina : MonoBehaviour
{
    public float CurrentStamina
    {
        get => _currentStamina;
        set
        {
            _currentStamina = Mathf.Clamp(value, 0, PlayerConfig.maxStamina);
            onStaminaChangeEvent?.Invoke(_currentStamina);
        }
    }
    private float _currentStamina;

    public Transform target;

    [SerializeField] private Vector3 offset;
    [SerializeField] private Slider staminaSlider;

    public Action<float> onStaminaChangeEvent;

    private Coroutine staminaRecoveryCoroutine;
    private Coroutine updateStaminaBarCoroutine;


    private void Awake()
    {
        StaminaBarState(false);
        onStaminaChangeEvent += OnStaminaChange;
    }
    private void OnDestroy()
    {
        onStaminaChangeEvent -= OnStaminaChange;
    }
    private void Update()
    {
        if (target != null)
            transform.position = target.position + offset;
    }

    #region Get Set Reference
    public void SetStaminaSliderInit(float minValue, float maxValue)
    {
        staminaSlider.minValue = minValue;
        staminaSlider.maxValue = maxValue;
        staminaSlider.value = staminaSlider.maxValue;
    }
    public void SetStaminaSlider(float currentValue)
    {
        staminaSlider.value = currentValue;
    }
    public void StaminaBarState(bool state) => staminaSlider.gameObject.SetActive(state);
    #endregion


    void OnStaminaChange(float value)
    {
        UpdateStamina(value);
    }

    public void UpdateStamina(float stamina)
    {
        if (stamina < PlayerConfig.maxStamina)
        {

            if (staminaRecoveryCoroutine != null) StopCoroutine(staminaRecoveryCoroutine);
            staminaRecoveryCoroutine = StartCoroutine(StaminaRecoveryCoroutine(stamina));

            if (updateStaminaBarCoroutine != null) StopCoroutine(updateStaminaBarCoroutine);
            updateStaminaBarCoroutine = StartCoroutine(UpdateStaminaBarCoroutine(stamina));
        }
    }

    IEnumerator UpdateStaminaBarCoroutine(float stamina)
    {
        //cập nhật thanh thể lực khi dash
        while (Mathf.RoundToInt(staminaSlider.value) > Mathf.RoundToInt(stamina))
        {
            staminaSlider.value -= PlayerConfig.staminaBarConsumeSpeed * Time.deltaTime;
            yield return null;
        }
        staminaSlider.value = stamina;
    }
    IEnumerator StaminaRecoveryCoroutine(float stamina)
    {
        StaminaBarState(true);

        float timer = PlayerConfig.staminaRecoveryWaitTime;

        //thời gian chờ đợi để chờ đợi 
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        //bắt đầu quá trình hồi phục
        while (timer <= 0 && staminaSlider.value < PlayerConfig.maxStamina)
        {
            _currentStamina += PlayerConfig.staminaRecoverySpeed * Time.deltaTime;

            staminaSlider.value += PlayerConfig.staminaRecoverySpeed * Time.deltaTime;
            
            yield return null;
        }

        _currentStamina = PlayerConfig.maxStamina;

        StaminaBarState(false);

    }
}
