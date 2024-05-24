using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    bool canMove = false;
    float moveSpeed = 5f;
    float countTimeMove = 0;
    public Rigidbody2D rb2d { get; private set; }
    public BossInfoReader BossInfo { get; private set; }
    public BossAttack BossAttack{ get; private set; }
    public BossHealth BossHealth{ get; private set; }
    public TargetDetector TargetDetector { get; private set; }
    public CapsuleCollider2D capsuleCollider2D { get; private set; }

    public Animator anim;

    private float indexTime = 2;


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        BossAttack = GetComponent<BossAttack>();
        BossInfo = GetComponent<BossInfoReader>();
        BossHealth = GetComponent<BossHealth>();
        TargetDetector = GetComponentInChildren<TargetDetector>();
    }

    private void Start()
    {
        StartCoroutine(AIManager());
    }
    private void Update()
    {
        if (canMove)
        {
            countTimeMove += Time.deltaTime;
            Vector2 direction = (TargetDetector.Target().position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, TargetDetector.Target().position, moveSpeed * Time.deltaTime);

            if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);
            else if (direction.x <= 0) transform.localScale = new Vector3(-1, 1, 1);

            if (Vector2.Distance(transform.position, TargetDetector.Target().position) <= 2 || countTimeMove >= 2)
            {
                countTimeMove = 0;
                canMove = false;
                anim.SetBool("Move", false);

                if(BossInfo.BossData.health <= (5000))
                {
                    anim.SetBool("Idle2", true);
                }
            }
        }
    }
    IEnumerator AIManager()
    {

        while (true)
        {
            yield return new WaitForSeconds(indexTime);

            if(BossInfo.BossData.health <= 0)
            {
                HandleBossDie();
                break;
            }

            if(BossInfo.BossData.health <= (5000))
            {
                Debug.Log("còn 50% hp");
                anim.SetBool("Idle2", true);
            }

            if (TargetDetector.Target() != null && BossAttack.canAtk)
            {
                indexTime = Random.Range(2, 5);
                ChangeState();
            }
        }
    }
    void ChangeState()
    {
        var randomIndex = Random.Range(0, 2); // số case
        switch (randomIndex)
        {
            case 0: Move(); break;
            case 1: Attack(); break;
        }
    }

    void Move()
    {
        if(Vector2.Distance(transform.position, TargetDetector.Target().position) >= 5)
        {
            canMove = true;
            anim.SetBool("Move", true);
        }
    }
    void Attack()
    {

        anim.SetTrigger("Attack");
        var randomSkillIndex = Random.Range(0, 3);

        switch (randomSkillIndex)
        {
            case 0: StartCoroutine(BossAttack.Skill1()); break;
            case 1: StartCoroutine(BossAttack.Skill2()); break;
            case 2: StartCoroutine(BossAttack.Skill3()); break;
        }
    }
    void Die()
    {
        anim.SetTrigger("Die");
    }

    void HandleBossDie()
    {
        Die();
        capsuleCollider2D.enabled = false;
    }
}
