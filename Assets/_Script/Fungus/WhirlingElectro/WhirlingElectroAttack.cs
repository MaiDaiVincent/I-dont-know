using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingElectroAttack : FungusAttack
{
    public WhirlingElectroES_Skill whirlingElectroES_Skill; 
    //Nếu vật triệu hồi còn tồn tại trên sân tạo sát thương hình ngôi sao
    public override void EB_Skill()
    {
        if (eBTimeIsCooling > 0) return;

        if (whirlingElectroES_Skill == null) return;

        whirlingElectroES_Skill.ObjDisable();

        SkillBase EB_SkillPrefab = EB_SkillConfig.skillPrefab;

        SkillBase EB_Skill;
        PoolType poolType = PoolType.WhirlingElectroEB_Skill;
        EB_Skill = PoolManager.Instance.SpawnObj(EB_SkillPrefab, transform.position, poolType);

        if (EB_Skill != null)
        {
            FungusInfoReader fungusInfo = fungusController.FungusInfo;

            Transform target = fungusController.TargetDetector.Target();

            EB_Skill.GetInfo(fungusInfo, EB_SkillConfig);
            EB_Skill.ShowcaseSkill(target, Vector2.zero);

            fungusManager.StartEB_Cooldown(this);

        }
    }

    //Tạo vật gây sát thương mục tiêu gần nhất mỗi 1s
    public override void ES_Skill()
    {
        if (eSTimeIsCooling > 0) return;

        SkillBase ES_SkillPrefab = ES_SkillConfig.skillPrefab;

        SkillBase ES_Skill;
        PoolType poolType = PoolType.WhirlingElectroES_Skill;
        ES_Skill = PoolManager.Instance.SpawnObj(ES_SkillPrefab, transform.position, poolType);

        if (ES_Skill != null)
        {
            whirlingElectroES_Skill = ES_Skill.GetComponent<WhirlingElectroES_Skill>();

            FungusInfoReader fungusInfo = fungusController.FungusInfo;
            Transform target = transform;

            ES_Skill.GetInfo(fungusInfo, ES_SkillConfig);
            ES_Skill.ShowcaseSkill(target, Vector2.zero);

            fungusManager.StartES_Cooldown(this);


        }
    }
}
