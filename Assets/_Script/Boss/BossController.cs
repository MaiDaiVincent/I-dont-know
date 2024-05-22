using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public Rigidbody2D rb2d { get; private set; }
    public BossInfoReader BossInfo { get; private set; }
    public BossAttack BossAttack{ get; private set; }
    public BossHealth BossHealth{ get; private set; }
    public TargetDetector TargetDetector { get; private set; }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();

        BossAttack = GetComponent<BossAttack>();
        BossInfo = GetComponent<BossInfoReader>();
        BossHealth = GetComponent<BossHealth>();
        TargetDetector = GetComponentInChildren<TargetDetector>();
    }
}
