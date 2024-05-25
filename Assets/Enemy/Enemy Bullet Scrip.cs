using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private GameObject fungus;
    private Rigidbody2D rb;
    public float force;
    public int damageAmount; // Số lượng máu mà Fungus mất khi bị va chạm với đạn
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fungus = GameObject.FindGameObjectWithTag("Fungus");

        Vector3 direction = fungus.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fungus"))
        {
            FungusHealth fungusHealth = other.gameObject.GetComponent<FungusHealth>();
            if (fungusHealth != null)
            {
                fungusHealth.TakeDamage(damageAmount);
            }

            Destroy(gameObject);
        }
    }
}