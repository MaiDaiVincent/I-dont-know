using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAmountHUD : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetAmountText(int enemyAmount, int bossAmount)
    {
        text.text = "Quái còn lại: \n" + enemyAmount + "\nBoss còn lại: \n" + bossAmount;
    }
}
