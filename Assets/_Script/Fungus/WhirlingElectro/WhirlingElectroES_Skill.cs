using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tạo vật gây sát thương mục tiêu gần nhất mỗi 1s

public class WhirlingElectroES_Skill : ES_Skill
{
    [SerializeField] private WhirlingElectroSummoning whirlingElectroSummoningPrefab;
    private WhirlingElectroSummoning whirlingElectroSummoning;
    private PoolType poolType = PoolType.WhirlingElectroSummoning;
    public Action<WhirlingElectroSummoning> OnSummoningEvent;
    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);

        whirlingElectroSummoning = poolManager.SpawnObj(whirlingElectroSummoningPrefab, transform.position, poolType);
        whirlingElectroSummoning.skillConfig = SkillConfig;
        whirlingElectroSummoning.fungusInfo = fungusInfo;
        OnSummoningEvent?.Invoke(whirlingElectroSummoning);
    }
    protected override IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(SkillConfig.activeTime);
        ObjDisable();
    }
    public void ObjDisable()
    {
        whirlingElectroSummoning.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
