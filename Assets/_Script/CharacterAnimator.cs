using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void SetMoveDirection(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    public void SetLastMoveDirection(Vector2 direction)
    {
        animator.SetFloat("LastMoveX", direction.x);
        animator.SetFloat("LastMoveY", direction.y);
    }

    public void SetAttackDirection(Vector2 direction)
    {
        animator.SetFloat("AttackX", direction.x);
        animator.SetFloat("AttackY", direction.y);
    }

    public void SetMoveState(bool state)
    {
        animator.SetBool("Move", state);
    }
    public void SetTriggerDie()
    {
        animator.SetTrigger("Die");
    }

    public void SetTriggerNA()
    {
        animator.SetTrigger("NA");
    }
    public void SetTriggerES()
    {
        animator.SetTrigger("ES");
    }
    public void SetTriggerEB()
    {
        animator.SetTrigger("EB");
    }

    //coming soon
    public void SetTriggerHurt()
    {
        animator.SetTrigger("Hurt");

    }
}
