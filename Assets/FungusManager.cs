using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class FungusManager : Singleton<FungusManager>
{
    public bool isHaveShield = false;
    public bool IsRecoveringInteractSlot { get; private set; } = false;
    public bool CanInteractSlot { get; private set; } = true;

    [Header("HUD")]
    [SerializeField] private FungusSkillHUD ESSkillHUD;
    [SerializeField] private FungusSkillHUD EBSkillHUD;
    [SerializeField] private FungusCurrentStatusHUD fungusCurrentStatusHUD;
    [SerializeField] private FungusSlotListHUD fungusSlotListHUD;
    public FungusStamina fungusStamina;

    [Header("Current Fungus Info")]
    public int CurrentSlotIndex;
    public FungusData CurrentFungusData;
    public FungusInfoReader CurrentFungusInfo;

    [Header("Fungus Slot")]
    [SerializeField] private List<FungusInfoReader> fungusInfoList;
    [SerializeField] private List<KeyCode> inputSlotFungus;

    private Coroutine recoverySwitchSlotCoroutine;
    private Coroutine fungusDieCoroutine;



    protected override void Awake()
    {
        base.Awake();
        onSpawnFungusInit += OnSpawnFungusInit;
    }
    private void OnDestroy()
    {
        onSpawnFungusInit -= OnSpawnFungusInit;
    }
    public void Update()
    {
        if (GameplayManager.Instance.isEndGame) return;
        InputSwitchSlot();
    }

    public void OnFungusDied()
    {
        if (CurrentFungusInfo.gameObject.activeSelf)
        {
            if (fungusDieCoroutine != null) StopCoroutine(fungusDieCoroutine);
            fungusDieCoroutine = StartCoroutine(FungusDieCoroutine());
        }
    }
    public void OnSpawnFungusInit(List<FungusInfoReader> fungusInfoList)
    {
        this.fungusInfoList = new List<FungusInfoReader>();
        this.fungusInfoList = fungusInfoList;
        Init();
    }


    public void Init()
    {
        fungusSlotListHUD.SetInit(fungusInfoList, inputSlotFungus);
        SwitchFungus(0);
        GetStaminaInit();
        GetSkillConfig();
    }

    public void InputSwitchSlot()
    {
        for (int i = 0; i < inputSlotFungus.Count; i++)
        {
            if (Input.GetKeyDown(inputSlotFungus[i]) && CanInputSwitchSlot())
            {
                SwitchFungus(i);
            }
        }
    }


    public void SwitchFungus(int slotIndex)
    {

        if (!CanSwitchFungus(slotIndex)) return;

        for (int i = 0; i < fungusInfoList.Count; i++)
        {
            if (fungusInfoList[i] == fungusInfoList[slotIndex])
            {
                fungusInfoList[i].gameObject.SetActive(true);
                ReloadCurrentFungus(slotIndex);

                if (recoverySwitchSlotCoroutine != null) StopCoroutine(recoverySwitchSlotCoroutine);
                recoverySwitchSlotCoroutine = StartCoroutine(RecoverySwitchSlotCoroutine());
            }
            else
            {
                fungusInfoList[i].gameObject.SetActive(false);
            }
        }

    }

    //Đặt lại thông tin Fungus sau khi chuyển đổi
    void ReloadCurrentFungus(int index)
    {

        if (CurrentSlotIndex != -1)
        {
            //Set lại vị trí cho Fungus
            Vector2 currentFungusPos = fungusInfoList[CurrentSlotIndex].transform.position;
            fungusInfoList[index].transform.position = currentFungusPos;

            //Đặt lại hướng cho Fungus
            FungusController oldFungusController = CurrentFungusInfo.FungusController;
            FungusController newFungusController = fungusInfoList[index].FungusController;
            newFungusController.AttackDirection = oldFungusController.AttackDirection;
            newFungusController.LastDirection = oldFungusController.LastDirection;
            newFungusController.MoveDirection = oldFungusController.MoveDirection;

        }

        //Đặt lại thông tin và chỉ số
        FungusInfoReader fungusInfo = fungusInfoList[index];
        CurrentFungusInfo = fungusInfo;
        CurrentSlotIndex = index;
        CurrentFungusData = fungusInfo.FungusData;

        //Đặt lại thông tin skill


        //Đặt lại trạng thái đang chọn cho Fungus slot hiện tại
        fungusSlotListHUD.SetSlotSelect(index);

        //thanh thể lực đổi mục tiêu 
        fungusStamina.target = fungusInfoList[index].transform;

        //Đặt lại mục tiêu cho camera
        EventManager.ActionOnCameraChangeTarget(fungusInfoList[index].transform);

        if (CurrentSlotIndex != -1)
        {
            //Gọi sự  kiện khi được chuyển đổi
            EventManager.ActionOnSwitchFungus(fungusInfoList[index], fungusCurrentStatusHUD);
        }

        CurrentFungusInfo.FungusController.FungusHealth.OnDiedEvent += OnFungusDied;
    }
    public void InteractSlotState(bool state)
    {
        if (!IsRecoveringInteractSlot)
        {
            CanInteractSlot = state;

            if (CanInteractSlot) SetFadeSlot(GameConfig.showSlotAlpha);
            else SetFadeSlot(GameConfig.fadeSlotAlpha);
        }
    }
    public void SetFadeSlot(float value)
    {
        foreach (var slot in fungusSlotListHUD.FungusSlotHUDList)
        {
            if (slot.CanActive) slot.SetFade(value);
            else slot.SetFade(GameConfig.inactiveSlotAlpha);

        }
    }
    public IEnumerator RecoverySwitchSlotCoroutine()
    {
        InteractSlotState(false);
        RecoveringInteractSlotState(true);

        float timer = GameConfig.switchSlotRecoveryTime;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            SetRecoveryInteractSlot(timer, true);
            yield return null;
        }

        timer = 0;

        SetRecoveryInteractSlot(timer, false);
        
        RecoveringInteractSlotState(false);

        if(FungusNotUsingSkill())
            InteractSlotState(true);
    }
    public void RecoveringInteractSlotState(bool state)
    {
        IsRecoveringInteractSlot = state;
    }
    public void SetRecoveryInteractSlot(float value, bool state)
    {
        foreach (var slot in fungusSlotListHUD.FungusSlotHUDList)
        {
            if (slot.CanActive)
            {
                slot.SetSlotInteractRecoveryState(state);
                slot.SetSlotInteractRecoverySlider(value);
                slot.SetSlotInteractRecoveryTime(value);
            }
            else
            {
                slot.SetSlotInteractRecoveryState(false);
                slot.SetSlotInteractRecoverySlider(0);
                slot.SetSlotInteractRecoveryTime(0);
            }
        }
    }

    void GetStaminaInit()
    {
        fungusStamina.CurrentStamina = PlayerConfig.maxStamina;
        fungusStamina.SetStaminaSliderInit(0, PlayerConfig.maxStamina);
    }


    public int GetFungusAliveIndex()
    {
        for (int i = 0; i < fungusInfoList.Count; i++)
        {
            FungusData fungusData = fungusInfoList[i].FungusData;
            if (fungusData.health != 0)
            {
                return i;
            }
        }
        GameplayUI.Instance.SetEnd();
        return -1;
    }

    public IEnumerator FungusDieCoroutine()
    {
        FungusController fungusController = CurrentFungusInfo.FungusController;

        fungusController.IsDied = true;
        fungusController.DiedState();

        int fungusAliveIndex = GetFungusAliveIndex();
        if (fungusAliveIndex >= 0)
        {
            yield return new WaitForSeconds(PlayerConfig.dyingWaitTime);

            SwitchFungus(fungusAliveIndex);
        }
        else
        {
            Debug.Log("Game over");
        }
    }



    bool FungusNotUsingSkill()
    {
        return !CurrentFungusInfo.FungusController.IsUsingSkill;
    }
    bool FungusAlive(int index)
    {
        FungusInfoReader fungusInfo = fungusInfoList[index];
        return !fungusInfo.FungusController.IsDied;
    }


    bool CanSwitchFungus(int index)
    {
        if (CurrentSlotIndex == -1) return true;

        return CurrentSlotIndex != index && FungusAlive(index);
    }


    public bool CanInputSwitchSlot()
    {
        return CanInteractSlot && !IsRecoveringInteractSlot 
            && FungusNotUsingSkill() && FungusAlive(CurrentSlotIndex);
    }

    public void GetSkillConfig()
    {
        foreach (var fungus in fungusInfoList)
        {
            FungusSkillConfig skillConfig = fungus.FungusData.skillConfig;
            if (skillConfig == null) return;

            FungusAttack fungusAttack = fungus.FungusController.FungusAttack;
            fungusAttack.GetSkillConfig(skillConfig);
        }
    }

    //cooldown

    public void StartES_Cooldown(FungusAttack fungusAttack)
    {
        StartCoroutine(ES_CooldownCoroutine(fungusAttack));

    }
    public void StartEB_Cooldown(FungusAttack fungusAttack)
    {
        StartCoroutine(EB_CooldownCoroutine(fungusAttack));

    }


    //ES
    public IEnumerator ES_CooldownCoroutine(FungusAttack fungusAttack)
    {
        float timer = fungusAttack.eSTime_Cooldown;

        SetES_CooldownState(timer, true, fungusAttack);

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            fungusAttack.eSTimeIsCooling = timer;
            SetES_CooldownState(timer, true, fungusAttack);

            yield return null;
        }
        fungusAttack.eSTimeIsCooling = 0;

        SetES_CooldownState(timer, false, fungusAttack);
        
    }

    //EB
    public IEnumerator EB_CooldownCoroutine(FungusAttack fungusAttack)
    {
        float timer = fungusAttack.eBTime_Cooldown;

        SetEB_CooldownState(timer, true, fungusAttack);

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            fungusAttack.eBTimeIsCooling = timer;
            SetEB_CooldownState(timer, true, fungusAttack);

            yield return null;
        }
        fungusAttack.eBTimeIsCooling = 0;

        SetEB_CooldownState(timer, false, fungusAttack);

    }

    public void SetES_CooldownState(float value, bool state, FungusAttack fungusAttack)
    {
        FungusInfoReader info = fungusAttack.fungusController.FungusInfo;

        if (CurrentFungusInfo != info) return;
        ESSkillHUD.SetES_CooldownState(value, state);
    }
    public void SetEB_CooldownState(float value, bool state, FungusAttack fungusAttack)
    {
        FungusInfoReader info = fungusAttack.fungusController.FungusInfo;

        if(CurrentFungusInfo != info) return;
        EBSkillHUD.SetEB_CooldownState(value, state);
    }
}
