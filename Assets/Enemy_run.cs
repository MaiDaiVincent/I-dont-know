using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_run : StateMachineBehaviour
{
    public float moveSpeed = 0.5f;

    Transform fungus;
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fungus = GameObject.FindGameObjectWithTag("Fungus").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 dir = (fungus.position - rb.transform.position);
        Vector3 faceEnemy = dir * moveSpeed * Time.deltaTime;
        
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
