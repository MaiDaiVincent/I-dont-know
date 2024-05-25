using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public LayerMask layerCheck;
    [SerializeField] private float radius;
    [SerializeField] private Collider2D[] colliderList = new Collider2D[0];

    public Transform Target()
    {
        colliderList = Physics2D.OverlapCircleAll(transform.position, radius, layerCheck);

        if (colliderList.Length == 0) return null;

        return colliderList[0].transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
