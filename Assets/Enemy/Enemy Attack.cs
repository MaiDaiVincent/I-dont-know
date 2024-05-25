using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class enemyattack : MonoBehaviour
{
    public float attackRange = 3f;
    public float attackDamage = 10f;
    public float attackCooldown = 2f;

    private Transform fungusTransform;
    private bool canAttack = true;

   

    private void Update()
    {
        GameObject fungus = GameObject.FindGameObjectWithTag("Fungus");
        if (fungus != null)
        {
            fungusTransform = fungus.transform;
        }
    
    
        if (fungusTransform != null && canAttack)
        {
            float distance = Vector3.Distance(transform.position, fungusTransform.position);
            if (distance <= attackRange)
            {
                AttackFungus();
            }
        }
    }

    private void AttackFungus()
    {
        // Di chuyển enemy đến vị trí của người chơi
        

       

        FungusHealth fungusHealth = fungusTransform.GetComponent<FungusHealth>();
        if (fungusHealth != null)
        {
            fungusHealth.TakeDamage((int)attackDamage);
        }
        Animator enemyAnimator = GetComponent<Animator>();
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Attack");
        }

        // Đặt thời gian chờ giữa các lần tấn công
        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}