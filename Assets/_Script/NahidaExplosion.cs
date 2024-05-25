using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NahidaExplosion : MonoBehaviour
{
    protected Coroutine disableCoroutine;

    private void OnEnable()
    {
        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCoroutine());
    }

    IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
