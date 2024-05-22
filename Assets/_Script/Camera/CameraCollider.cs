using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : Singleton<CameraCollider>
{
    [SerializeField] private Vector2 checkArea;
    [SerializeField] private LayerMask layerMask;

    protected override void Awake()
    {
        base.Awake();

        _GetCameraSize();
    }
    private void _GetCameraSize()
    {

        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            float cameraHeight = mainCamera.orthographicSize * 2f;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            checkArea = new Vector2(cameraWidth, cameraHeight);
        }
    }
    public Collider2D CheckTargetDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, checkArea, 0, layerMask);
        if(colliders.Length > 0)
        {
            return colliders[0];
        }
        return null;
    }

    public Transform GetTargetTransform()
    {
        if (CheckTargetDetection())
        {
            return CheckTargetDetection().transform;
        }
        return null;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, checkArea);
    }
}
