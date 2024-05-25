using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : HealthBase
{
    public int maxHealth = 1000;
    private int currentHealth;
    private Animator animator;
    public bool isDead;
    public float destroyDelay = 1f; // Thời gian chờ trước khi hủy gameObject

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
            Destroy(gameObject, destroyDelay);
            SpawnerManager.Instance.SpawnWave();
            GameplayUI.Instance.KillEnemy();
        }
    }
}