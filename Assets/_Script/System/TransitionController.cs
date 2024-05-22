using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    public void TransitionPanelState(bool state)
    {
        canvasGroup.gameObject.SetActive(state);
    }
}
