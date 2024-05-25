using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;

    private float timer;
    private GameObject fungus;


    
    private void Update()
    {
        fungus = GameObject.FindGameObjectWithTag("Fungus");
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, fungus.transform.position);
        Debug.Log(distance);
        if (distance < 4)
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
        
        void shoot()
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
}