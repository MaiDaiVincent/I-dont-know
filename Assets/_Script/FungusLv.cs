using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FungusLv : MonoBehaviour
{
    private FungusInfoReader fungusInfo;
    private FungusCurrentStatusHUD fungusCurrentStatusHUD;
    private void Awake()
    {
        fungusInfo = GetComponent<FungusInfoReader>();

    }
    private void OnDestroy()
    {
    }
    private void Update()
    {

    }
}
