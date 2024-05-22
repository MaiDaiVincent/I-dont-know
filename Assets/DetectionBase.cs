using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionBase : MonoBehaviour
{
    public LayerMask layerCheck;

    public UnityEvent<GameObject> detectionEnterEvent;
    public UnityEvent<GameObject> detectionStayEvent;
}
