using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingCyroAttack : FungusAttack
{
    //tạo vùng gây sát thương nguyên tố băng mỗi 0.5 giây
    public override void EB_Skill()
    {
        if (eBTimeIsCooling > 0) return;

        SkillBase EB_SkillPrefab = EB_SkillConfig.skillPrefab;

        SkillBase EB_Skill;
        PoolType poolType = PoolType.WhirlingCyroEB_Skill;
        EB_Skill = PoolManager.Instance.SpawnObj(EB_SkillPrefab, transform.position, poolType);

        if (EB_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;

            Transform target = transform;

            EB_Skill.GetInfo(fungusInfo, EB_SkillConfig);
            EB_Skill.ShowcaseSkill(target, Vector2.one);

            fungusManager.StartEB_Cooldown(this);

        }
    }

    //tạo khiên băng chống đỡ sát thương
    public override void ES_Skill()
    {
        if (eSTimeIsCooling > 0) return;

        AudioManager.Instance.PlayBuff();

        SkillBase ES_SkillPrefab = ES_SkillConfig.skillPrefab;

        SkillBase ES_Skill;
        PoolType poolType = PoolType.WhirlingCyroES_Skill;
        ES_Skill = PoolManager.Instance.SpawnObj(ES_SkillPrefab, transform.position, poolType);
        if (ES_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;
            Transform target = transform;

            ES_Skill.GetInfo(fungusInfo, ES_SkillConfig);
            ES_Skill.ShowcaseSkill(target, Vector2.one);

            fungusManager.StartES_Cooldown(this);

        }
    }
}
