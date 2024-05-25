using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : MonoBehaviour, ISkill
{
    #region Variables
    public bool CanCrit { get; set; } = false;
    public int ValuePercent { get; set; } = 100;
    public int BaseValue { get; set; }
    public int CritDamage { get; set; }
    public float CritDamagePercent { get; set; }
    public float MoveSpeed { get; set; }
    public float CritRate { get; set; }
    public Color PopUpColor { get; set; }

    public Transform Target { get; set; }
    public Vector3 Direction { get; set; }

    public SkillConfig SkillConfig { get; private set; }
    public FungusInfoReader fungusInfo { get; private set; }

    protected TriggerDetection triggerDetection;
    protected PoolManager poolManager => PoolManager.Instance;
    protected CameraController cameraController => CameraController.Instance;

    protected Coroutine disableCoroutine;


    #endregion

    protected virtual void Awake()
    {
        triggerDetection = GetComponent<TriggerDetection>();
        _ListenEvents();
    }

    protected virtual void _ListenEvents()
    {
        if (triggerDetection == null) return;
        triggerDetection.detectionEnterEvent.AddListener((GameObject obj) => EnterDetection(obj));
        triggerDetection.detectionStayEvent.AddListener((GameObject obj) => StayDetection(obj));
    }

    protected virtual void StayDetection(GameObject obj) { }

    protected virtual void EnterDetection(GameObject obj)
    {
        if (obj.GetComponent<HealthBase>())
        {
            CauseDamage(obj);
        }

        gameObject.SetActive(false);
    }

    protected void CauseDamage(GameObject obj)
    {
        Vector3 collisionPos = obj.transform.position;
        CanCrit = Helper.CanCrit(CritRate);
        CritDamage = Helper.CritDamage(BaseValue, CritDamagePercent);
        int damage = Helper.CauseDamage(BaseValue, CanCrit, CritDamage, ValuePercent);

        obj.GetComponent<HealthBase>().TakeDamage(damage);

        TextPopUp textPopUp;
        textPopUp = poolManager.SpawnObj(poolManager.GetTextPopUp(), collisionPos, PoolType.TextPopUp);
        textPopUp.SetPopUpDamage(damage, CanCrit, PopUpColor);

        CameraShake.instance.Shake(1, 0.2f);
    }
    public virtual void ShowcaseSkill(Transform target, Vector2 direction)
    {
        Target = target;
        Direction = direction;

        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCoroutine());
    }

    public virtual void GetInfo(FungusInfoReader info, SkillConfig skillConfig)
    {
        fungusInfo = info;
        SkillConfig = skillConfig;

        GetStats();
        GetPopUpColor();

    }
    public virtual void GetStats()
    {
        FungusData data = fungusInfo.FungusData;

        BaseValue = data.atk;
        CanCrit = Helper.CanCrit(data.critRate);
        CritDamage = Helper.CritDamage(data.atk, data.critDamagePercent);

        CritRate = data.critRate;
        CritDamagePercent = data.critDamagePercent;
        ValuePercent = SkillConfig.valuePercent;
        MoveSpeed = SkillConfig.moveSpeed;
    }
    public virtual void GetPopUpColor()
    {
        Color popUpColor = fungusInfo.FungusData.fungusConfig.fungusColor;
        PopUpColor = popUpColor;
    }
    public virtual void MoveToTarget() { }

    protected virtual IEnumerator DisableCoroutine()
    {
        yield return null;
    }
}
