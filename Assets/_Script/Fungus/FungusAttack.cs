using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AttackType
{
    NA,
    ES,
    EB
}
public abstract class FungusAttack : MonoBehaviour
{
    public float eSTimeIsCooling;
    public float eBTimeIsCooling;

    public float eSTime_Cooldown;
    public float eBTime_Cooldown;
    public SkillConfig NA_SkillConfig { get; set; }
    public SkillConfig ES_SkillConfig { get; set; }
    public SkillConfig EB_SkillConfig { get; set; }

    public FungusController fungusController { get; private set; }
    protected CameraCollider cameraCollider => CameraCollider.Instance;
    protected FungusManager fungusManager => FungusManager.Instance;

    protected virtual void Awake()
    {
        fungusController = GetComponent<FungusController>();
    }
    public void GetSkillConfig(FungusSkillConfig fungusSkillConfig)
    {
        NA_SkillConfig = fungusSkillConfig.nA_SkillConfig;
        ES_SkillConfig = fungusSkillConfig.eS_SkillConfig;
        EB_SkillConfig = fungusSkillConfig.eB_SkillConfig;

        eSTime_Cooldown = ES_SkillConfig.cooldown;
        eBTime_Cooldown = EB_SkillConfig.cooldown;

        fungusManager.SetES_CooldownState(0, false, this);
        fungusManager.SetEB_CooldownState(0, false, this);

    }
    public virtual void NA_Skill()
    {

        AudioManager.Instance.PlayFungusShoot();


        SkillBase NA_SkillPrefab = NA_SkillConfig.skillPrefab;

        SkillBase NA_Skill;
        NA_Skill = PoolManager.Instance.SpawnObj(NA_SkillPrefab, transform.position, PoolType.FloatingHydroNA_Skill);

        if (NA_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;
            
            Transform target = cameraCollider.GetTargetTransform();
            Vector2 direction = fungusController.DirectionAttackWithOutTarget();

            NA_Skill.GetInfo(fungusInfo, NA_SkillConfig);
            NA_Skill.ShowcaseSkill(target, direction);

        }
    }
    public abstract void ES_Skill();
    public abstract void EB_Skill();
}
