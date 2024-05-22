using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private BossBullet bulletPrefab;

    private BossController bossController;
    public bool canAtk;
    private void Awake()
    {
        bossController = GetComponent<BossController>();
    }
    private void Start()
    {
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        while (canAtk)
        {
            yield return new WaitForSeconds(1f);

            while (bossController.TargetDetector.Target() == null) yield return null;

            BossBullet bullet = PoolManager.Instance.SpawnObj(bulletPrefab, transform.position, PoolType.BossBullet);
            if(bullet != null)
            {
                bullet.target = bossController.TargetDetector.Target();
                bullet.MoveToTarget();
                bullet.GetBossInfo(bossController.BossInfo);
            }
        }
    }
}
