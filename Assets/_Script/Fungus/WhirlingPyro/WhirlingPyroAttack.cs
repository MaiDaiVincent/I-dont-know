using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingPyroAttack : FungusAttack
{
    public override void EB_Skill()
    {
        if (eBTimeIsCooling > 0) return;

        Debug.Log("eb");

        SkillBase EB_SkillPrefab = EB_SkillConfig.skillPrefab;

        SkillBase EB_Skill;
        EB_Skill = PoolManager.Instance.SpawnObj(EB_SkillPrefab, transform.position, PoolType.WhirlingPyroEB_Skill);

        if (EB_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;

            Transform target = fungusController.TargetDetector.Target();

            EB_Skill.GetInfo(fungusInfo, EB_SkillConfig);
            EB_Skill.ShowcaseSkill(target, Vector2.down);

            fungusManager.StartEB_Cooldown(this);
        }
    }

    public override void ES_Skill()
    {
        if (eSTimeIsCooling > 0) return;

        SkillBase ES_SkillPrefab = ES_SkillConfig.skillPrefab;

        SkillBase ES_Skill;
        ES_Skill = PoolManager.Instance.SpawnObj(ES_SkillPrefab, transform.position, PoolType.WhirlingPyroES_Skill);

        if (ES_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;
            Transform target = transform;

            ES_Skill.GetInfo(fungusInfo, ES_SkillConfig);
            ES_Skill.ShowcaseSkill(target, Vector2.zero);

            fungusManager.StartES_Cooldown(this);

        }
    }
}
