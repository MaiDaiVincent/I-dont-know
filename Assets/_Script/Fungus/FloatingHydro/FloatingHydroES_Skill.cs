using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

//b?n ??n ??nh v? m?c tiêu
public class FloatingHydroES_Skill : ES_Skill
{
    [SerializeField] private FloatingHydroES_Explosion floatingHydroES_Explosion;

    private Rigidbody2D rb2d;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        FloatingHydroES_Explosion ES_Explosion;

        PoolType poolType = PoolType.FloatingHydroES_Explosion;
        ES_Explosion = poolManager.SpawnObj(floatingHydroES_Explosion, transform.position, poolType);

        int damage = Helper.CauseDamage(BaseValue, CanCrit, CritDamage, ValuePercent);
        ES_Explosion.GetInfo(damage, CanCrit, PopUpColor);
    }
    public void SetTarget(Transform target)
    {
        Target = target;
    }
    public void SetRotation(Vector2 direction)
    {
        transform.rotation = Helper.RotationDirection(direction);
    }
    public override void MoveToTarget()
    {
        if (Target != null)
        {
            Direction = Helper.TargetDirection(Target, transform);
        }
        rb2d.velocity = Direction * MoveSpeed;
    }
    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);
        SetRotation(direction);
        MoveToTarget();

    }
}
