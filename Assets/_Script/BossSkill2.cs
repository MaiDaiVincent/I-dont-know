using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill2 : BossSkill
{
    public override void ShowCaseSkill()
    {
        if (target == null) return;

        circleCollider2D.enabled = false;
        transform.position = target.position;

        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCoroutine());
    }
    protected override void TriggerDetection(GameObject @object)
    {
        if (@object.GetComponent<HealthBase>())
        {
            int atk = bossInfo.BossData.damage;
            @object.GetComponent<HealthBase>().TakeDamage(atk * 5);
            CameraShake.instance.Shake(3, 0.2f);
            gameObject.SetActive(false);
        }
    }

    protected IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        circleCollider2D.enabled = true;

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
