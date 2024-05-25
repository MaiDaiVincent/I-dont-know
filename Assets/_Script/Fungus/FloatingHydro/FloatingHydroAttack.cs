using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHydroAttack : FungusAttack
{
    public override void EB_Skill()
    {
        if (eBTimeIsCooling > 0) return;

        AudioManager.Instance.PlayFungusShoot();

        SkillBase EB_SkillPrefab = EB_SkillConfig.skillPrefab;

        SkillBase EB_Skill;
        EB_Skill = PoolManager.Instance.SpawnObj(EB_SkillPrefab, transform.position, PoolType.FloatingHydroEB_Skill);

        if (EB_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;

            Transform target = fungusController.TargetDetector.Target();

            Vector2 direction = Vector2.zero;
            if (target == null)
            {
                direction = fungusController.DirectionAttackWithOutTarget();
            }
            else
            {
                direction = Helper.TargetDirection(target, transform);
            }

            EB_Skill.GetInfo(fungusInfo, EB_SkillConfig);
            EB_Skill.ShowcaseSkill(target, direction);

            fungusManager.StartEB_Cooldown(this);
        }
    }

    public override void ES_Skill()
    {
        if (eSTimeIsCooling > 0) return;

        AudioManager.Instance.PlayFungusShoot();


        SkillBase ES_SkillPrefab = ES_SkillConfig.skillPrefab;

        SkillBase ES_Skill;
        ES_Skill = PoolManager.Instance.SpawnObj(ES_SkillPrefab, transform.position, PoolType.FloatingHydroES_Skill);

        if(ES_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;
            Transform target = fungusController.TargetDetector.Target();

            Vector2 direction = Vector2.zero;
            if(target == null)
            {
                direction = fungusController.DirectionAttackWithOutTarget();
            }
            else
            {
                direction = Helper.TargetDirection(target, transform);
            }

            ES_Skill.GetInfo(fungusInfo, ES_SkillConfig);
            ES_Skill.ShowcaseSkill(target, direction);

            fungusManager.StartES_Cooldown(this);

        }
    }
}
