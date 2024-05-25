using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthSlider;
    private Animator animator;
    private bool isDead;
    public float destroyDelay = 1f; // Thời gian chờ trước khi hủy gameObject

    private void Start()
    {
        currentHealth = maxHealth;
        healthSlider.value = 1f;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        healthSlider.value = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
            Destroy(gameObject, destroyDelay);
        }
    }
}