using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//buff atk theo th?i gian
public class WhirlingPyroES_Skill : ES_Skill
{
    private int buffAtkValue;

    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);
        buffAtkValue = Helper.BuffAtk(BaseValue, ValuePercent);
        fungusInfo.FungusData.atk += buffAtkValue;
    }
    protected override IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(SkillConfig.activeTime);
        gameObject.SetActive(false);
        fungusInfo.FungusData.atk -= buffAtkValue;
    }
}
