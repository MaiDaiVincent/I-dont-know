using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    public bool roaming = true;
    public float moveSpeed;
    public float nextWPDistance;
    public Seeker seeker;
    public bool updateContinuesPath;
    bool reachDestination = false; 
    Path path;
    Coroutine movecoroutine;

    private void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if (seeker.IsDone() && (reachDestination || updateContinuesPath)) 
        seeker.StartPath(transform.position, target, onPathConplete);

    }
    void onPathConplete(Path p)
    {
        if (p.error) return;
        path = p;
        //Move to target
        MoveToTarget();
    }
    void MoveToTarget()
    {
        if (movecoroutine != null) StopCoroutine(movecoroutine);
        movecoroutine = StartCoroutine(MoveToTargetCoroutine());

    }
    IEnumerator MoveToTargetCoroutine()
    {
        int currenWP = 0;
        reachDestination = false;
        while (currenWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currenWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currenWP]);
            if (distance < nextWPDistance)
                currenWP++;
            yield return null;
        }
        reachDestination = true;
    }
    Vector2 FindTarget()
    {
        Vector3 playerPos = FindObjectOfType<CharacterAnimator>().transform.position;
        if (roaming == true)
        {
            return (Vector2) playerPos + (Random.Range(10f,50f) * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
        }
        else
        {
            return playerPos;
        }
    }
 
    
}
