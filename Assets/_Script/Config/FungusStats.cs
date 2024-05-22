using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fungus Stat", menuName = "Stats/Fungus")]
public class FungusStats : ScriptableObject
{
    public float maxStamina = 100; 

    public float maxHealth = 10000;

    public int atk = 10000;

    public int lv = 1;

    public float moveSpeed = 3f;

    public float dashTime = 0.2f;
    public float dashForce = 15f;
    public int dashStamina = 20;

    public float critRatePercent = 5f;
    public float critDamagePercent = 50f;
    public int elementalMastery = 0;
}
