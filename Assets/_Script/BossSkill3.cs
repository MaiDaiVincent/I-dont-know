using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill3 : BossSkill
{
    public NahidaExplosion nahidaExplosion;
    float radius = 3f;
    public override void ShowCaseSkill()
    {
        if (target == null) return;

        circleCollider2D.enabled = false;
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        Vector3 newPosition = new Vector3(randomPoint.x, randomPoint.y, 0) + target.position;
        transform.position = newPosition;


        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCoroutine());
    }
    protected override void TriggerDetection(GameObject @object)
    {
        if (@object.GetComponent<HealthBase>())
        {
            int atk = bossInfo.BossData.damage;
            @object.GetComponent<HealthBase>().TakeDamage(atk);
            CameraShake.instance.Shake(3, 0.2f);
            gameObject.SetActive(false);
        }
    }
    protected IEnumerator DisableCoroutine()
    {

        yield return new WaitForSeconds(0.9f);
        circleCollider2D.enabled = true;
        var explosion = PoolManager.Instance.SpawnObj(nahidaExplosion, transform.position, PoolType.BossExplosion);

        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
