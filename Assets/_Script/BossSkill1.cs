using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill1 : BossSkill
{
    float speed = 5;
    public float rotation = 0;

    public override void ShowCaseSkill()
    {
        if (target == null) return;

        rotation = 0;
        MoveToTarget();

        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCoroutine());
    }
    private void Update()
    {
        if (rotation >= 360) rotation = 0;
        rotation += Time.deltaTime * 720;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
    public void MoveToTarget()
    {
        if (target == null)
        {
            rb2d.velocity = Vector2.zero * speed;
            return;
        }
        Vector3 targetPos = target.transform.position;
        direction = (targetPos - transform.position).normalized;
        rb2d.velocity = direction * speed;
    }

    protected override void TriggerDetection(GameObject @object)
    {
        if (@object.GetComponent<HealthBase>())
        {
            int atk = bossInfo.BossData.damage;
            @object.GetComponent<HealthBase>().TakeDamage(atk);
            CameraShake.instance.Shake(2, 0.2f);
            gameObject.SetActive(false);
        }
    }

    protected IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
