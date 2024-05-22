using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public static Vector2 TargetDirection(Transform target, Transform transform)
    {
        return (target.position - transform.position).normalized;
    }
    public static Quaternion RotationDirection(Vector2 direction)
    {
        var rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(new Vector3(0, 0, rotation));
    }
    public static bool CanCrit(float critRate)
    {
        float minCrit = 0;
        float maxCrit = 100;
        float value = Random.Range(minCrit, maxCrit +1);
        if (value <= critRate || critRate >= maxCrit) return true;
        return false;
    }
    public static int CritDamage(int damage, float critDamagePercent)
    {
        float critDamage = (damage * (critDamagePercent/100));
        return (int)critDamage;
    }
    public static int CauseDamage(int atk, bool canCrit, int critDamage, int valuePercent)
    {
        int finalDamage = atk;
        if (canCrit) finalDamage += critDamage;
        finalDamage = (int)(finalDamage * (float)(valuePercent / 100f));
        return finalDamage;
    }

    public static int ShieldValue(int health, float valuePercent)
    {
        int shieldValue = (int)(health * (valuePercent / 100f));
        return shieldValue;
    }
    public static int BuffAtk(int baseDamage, float valuePercent)
    {
        int buffDamageValue = (int)(baseDamage * (valuePercent / 100f));
        return buffDamageValue - baseDamage;
    }
}
