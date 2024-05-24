using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill : MonoBehaviour
{
    public Transform target;
    public Vector2 direction;

    protected Rigidbody2D rb2d;
    protected BossInfoReader bossInfo;
    protected TriggerDetection triggerDetection;
    protected Coroutine disableCoroutine;
    protected CircleCollider2D circleCollider2D;
    protected PoolManager poolManager => PoolManager.Instance;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        triggerDetection = GetComponent<TriggerDetection>();
        circleCollider2D = GetComponent<CircleCollider2D>();

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

    protected virtual void TriggerDetection(GameObject @object) { }
    public virtual void ShowCaseSkill() { }
}
