using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour
{
    public void startA(float a, Test test)
    {
        StartCoroutine(countA(a, test));

    }
    public void startB(float b, Test test)
    {
        StartCoroutine(countB(b, test));

    }
    public IEnumerator countA(float a, Test test)
    {
        float timer = a;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            test.coolingA = timer;
            yield return null;
        }
        test.coolingA = 0;
        
    }
    public IEnumerator countB(float a, Test test)
    {
        float timer = a;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            test.coolingB = timer;

            yield return null;
        }
        test.coolingB = 0;

    }
}
