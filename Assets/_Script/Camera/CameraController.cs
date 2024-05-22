using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class CameraController : Singleton<CameraController>
{
    public Transform Target { get; set; }
    [SerializeField] private float smoothSpeed = 0.125f;


    protected override void Awake()
    {
        base.Awake();
        EventManager.onCameraChangeTarget += OnCameraChangeTarget;
    }
    private void OnDestroy()
    {
        EventManager.onCameraChangeTarget -= OnCameraChangeTarget;
    }
    private void FixedUpdate()
    {
        if (Target == null) return;
        Vector3 desiredPosition = Target.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    void OnCameraChangeTarget(Transform target)
    {
        this.Target = target;
    }
}
