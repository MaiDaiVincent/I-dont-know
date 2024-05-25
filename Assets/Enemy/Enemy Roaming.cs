using UnityEngine;

public class EnemyRoaming : MonoBehaviour
{
    private Transform target;
    public float roamRadius = 10f;
    public float moveSpeed = 3f;

    private Vector3 targetPosition;
    private bool isRoaming = true;
    public SpriteRenderer enemySR;



    private void Update()
    {
        // Đặt vị trí mục tiêu ban đầu là vị trí của currentFungusInfo
        if (FungusManager.Instance != null && FungusManager.Instance.CurrentFungusInfo != null)
        {
            target = FungusManager.Instance.CurrentFungusInfo.transform;
            isRoaming = false; // Không di chuyển roaming, di chuyển tới mục tiêu
        }
        else
        {
            // Nếu không có currentFungusInfo, di chuyển theo roaming
            targetPosition = GetRandomRoamingPosition();
        }
        if (isRoaming)
        {
            // Kiểm tra xem enemy đã đến gần vị trí mục tiêu roaming chưa
            if (Vector3.Distance(transform.position, targetPosition) <= 0.2f)
            {
                // Nếu đã đến gần, chọn vị trí mục tiêu mới ngẫu nhiên trong phạm vi roamRadius
                targetPosition = GetRandomRoamingPosition();
            }

            // Di chuyển enemy tới vị trí mục tiêu roaming
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Di chuyển enemy tới vị trí mục tiêu (currentFungusInfo)
            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                direction.Normalize();
                transform.Translate(direction * moveSpeed * Time.deltaTime);
                if (direction.x != 0)
                {
                    if (direction.x < 0)
                    {
                        enemySR.transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        enemySR.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
        }
    }

    private Vector3 GetRandomRoamingPosition()
    {
       
        Vector3 randomDirection = Random.insideUnitCircle * roamRadius;
        randomDirection += transform.position;
        randomDirection.z = transform.position.z; // Giữ nguyên chiều cao Z
        return randomDirection;
    }
}