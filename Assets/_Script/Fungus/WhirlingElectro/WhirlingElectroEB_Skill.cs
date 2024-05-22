using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingElectroEB_Skill : EB_Skill
{
    [SerializeField] private WhirlingElectroLightningPro whirlingElectroLightningProPrefab;
    private WhirlingElectroLightningPro whirlingElectroLightningPro;
    private Coroutine attackCoroutine;
    private PoolType poolType = PoolType.WhirlingElectroLightningPro;

    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);

        if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("a");
            whirlingElectroLightningPro = poolManager.SpawnObj(whirlingElectroLightningProPrefab, Target.position, poolType);
            whirlingElectroLightningPro.FungusInfo = fungusInfo;
            whirlingElectroLightningPro.SkillConfig = SkillConfig;

            yield return new WaitForSeconds(i == 0 ? 1f : 0.5f);
        }

    }
}
