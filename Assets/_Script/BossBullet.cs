using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public Transform target;
    public Vector2 direction;
    [SerializeField] private float speed = 1;

    private Rigidbody2D rb2d;
    private BossInfoReader bossInfo;
    private TriggerDetection triggerDetection;
    private PoolManager poolManager => PoolManager.Instance;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        triggerDetection = GetComponent<TriggerDetection>();

        _ListenEvents();
    }

    void _ListenEvents()
    {
        triggerDetection.detectionEnterEvent.AddListener((GameObject obj) => TriggerDetection(obj));

    }
    public void GetBossInfo(BossInfoReader bossInfo)
    {
        this.bossInfo = bossInfo;
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

    void TriggerDetection(GameObject @object)
    {
        if (@object.GetComponent<HealthBase>())
        {
            int atk = bossInfo.BossData.damage;
            @object.GetComponent<HealthBase>().TakeDamage(atk);
        }

        gameObject.SetActive(false);

        if (@object.GetComponent<SkillBase>())
        {

        }

    }
}
