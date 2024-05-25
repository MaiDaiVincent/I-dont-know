using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public BossSkill bossSkill1;
    public BossSkill bossSkill2;
    public BossSkill bossSkill3;

    private BossController bossController;
    public bool canAtk;
    private void Awake()
    {
        bossController = GetComponent<BossController>();
    }
    private void Start()
    {

        canAtk = true;
    }
    public IEnumerator Skill1()
    {
        canAtk = false;

        var random = Random.Range(5, 10);
        for(int i=0; i< random; i++)
        {
            yield return new WaitForSeconds(0.2f);
            AudioManager.Instance.PlayBossSkill1();
            SpawnSkill1();
        }
        canAtk = true;

    }
    public IEnumerator Skill2()
    {
        canAtk = false;
        var random = Random.Range(3, 5);
        for (int i = 0; i < random; i++)
        {
            yield return new WaitForSeconds(0.8f);
            AudioManager.Instance.PlayBossSkill2();
            SpawnSkill2();
        }
        canAtk = true;
    }
    public IEnumerator Skill3()
    {
        canAtk = false;

        var random = Random.Range(10, 12);

        for (int i = 0; i < random; i++)
        {
            yield return new WaitForSeconds(0.3f);
            AudioManager.Instance.PlayBossSkill3();
            SpawnSkill3();
        }
        canAtk = true;
    }
    void SpawnSkill1()
    {
        if (bossController.TargetDetector.Target() == null) return;

        BossSkill bossSkill = PoolManager.Instance.SpawnObj(bossSkill1, transform.position, PoolType.BossSkill1);
        if (bossSkill != null)
        {
            bossSkill.target = bossController.TargetDetector.Target();
            bossSkill.GetBossInfo(bossController.BossInfo);
            bossSkill.ShowCaseSkill();
        }
    }
    void SpawnSkill2()
    {
        if (bossController.TargetDetector.Target() == null) return;


        BossSkill bossSkill = PoolManager.Instance.SpawnObj(bossSkill2, transform.position, PoolType.BossSkill2);
        if (bossSkill != null)
        {
            bossSkill.target = bossController.TargetDetector.Target();
            bossSkill.GetBossInfo(bossController.BossInfo);
            bossSkill.ShowCaseSkill();
        }
    }
    void SpawnSkill3()
    {
        if (bossController.TargetDetector.Target() == null) return;


        BossSkill bossSkill = PoolManager.Instance.SpawnObj(bossSkill3, transform.position, PoolType.BossSkill3);
        if (bossSkill != null)
        {
            bossSkill.target = bossController.TargetDetector.Target();
            bossSkill.GetBossInfo(bossController.BossInfo);
            bossSkill.ShowCaseSkill();
        }
    }
}
