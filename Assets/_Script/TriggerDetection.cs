using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : DetectionBase
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerCheck == (layerCheck | (1 << collision.gameObject.layer)))
        {
            detectionEnterEvent.Invoke(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (layerCheck == (layerCheck | (1 << collision.gameObject.layer)))
        {
            detectionStayEvent.Invoke(collision.gameObject);
        }
    }
}
