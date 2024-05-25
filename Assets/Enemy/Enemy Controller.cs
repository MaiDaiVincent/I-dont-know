using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    [SerializeField] float moveSpeed = 0.5f;
    public SpriteRenderer enemySR; 
   

    private void Update()
    {
        if (FungusManager.Instance != null && FungusManager.Instance.CurrentFungusInfo != null)
        {
            // Lấy transform của currentFungusInfo
            target = FungusManager.Instance.CurrentFungusInfo.transform;
        }

        if (target != null)
        {
            // Tính toán hướng di chuyển tới vị trí của currentFungusInfo
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // Di chuyển enemy theo hướng tính toán
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            if(direction.x != 0)
            {
                if(direction.x < 0)
                {
                    enemySR.transform.localScale = new Vector3(-1, 1, 1);
                }else
                {
                    enemySR.transform.localScale = new Vector3(1, 1, 1);
                }
            }


        }
    }
}