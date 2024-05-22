using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

//t?o khiên theo th?i gian
public class WhirlingCyroES_Skill : ES_Skill
{
    public WhirlingCyroShield whirlingCyroShieldPrefab;
    private WhirlingCyroShield whirlingCyroShield;
    
    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);
        if (Target == null) return;

        PoolType poolType = PoolType.WhirlingCyroShield;
        whirlingCyroShield = poolManager.SpawnObj(whirlingCyroShieldPrefab, Target.position, poolType);
        
        whirlingCyroShield.Target = Target;
        int health = (int)fungusInfo.FungusData.health;
        whirlingCyroShield.Health = Helper.ShieldValue(health, ValuePercent);
    }
    protected override IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(SkillConfig.activeTime);
        if(whirlingCyroShield != null) whirlingCyroShield.gameObject.SetActive(false);
        gameObject.SetActive(false);

    }
}
