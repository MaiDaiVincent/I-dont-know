using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Stat", menuName = "Stats/Boss")]
public class BossStats : ScriptableObject
{
    public float maxHealth = 10000;

    public int damage = 10000;

    public int lv = 1;

    public float moveSpeed = 3f;

    public float critRate = 5f;
    public float critDamage = 50f;

}
