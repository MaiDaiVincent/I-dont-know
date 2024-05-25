using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float a = 10;
    public float b = 20;
    public float coolingA;
    public float coolingB;
    public test2 test2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            test2.startA(a, this);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            test2.startB(b, this);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.SetActive(false);
        }
    }
}
