using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NA_Skill : SkillBase
{
    #region Variables
    [SerializeField] private NA_SkillExplosion NAExplosionPrefab;

    private Rigidbody2D rb2d;
    private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;
    private Gradient gradientParticle;
    private Gradient gradientBullet;

    #endregion
    protected override void Awake()
    {
        base.Awake();

        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        trailRenderer.Clear();
    }

    #region Get Set Reference
    public override void GetInfo(FungusInfoReader info, SkillConfig skillConfig)
    {
        base.GetInfo(info, skillConfig);
        FungusConfig config = fungusInfo.FungusData.fungusConfig;
        GetConfig(config.fungusColor, config.gradientParticle, config.gradientBullet);
    }

    void GetConfig(Color color, Gradient gParticle, Gradient gBullet)
    {
        gradientBullet = gBullet;
        gradientParticle = gParticle;

        spriteRenderer.color = color;
        trailRenderer.colorGradient = gBullet;
    }
    #endregion

    public override void MoveToTarget()
    {
        if (Target != null)
        {
            Direction = Helper.TargetDirection(Target, transform);
        }
        rb2d.velocity = Direction * MoveSpeed;
    }

    protected override void EnterDetection(GameObject @object)
    {
        base.EnterDetection(@object);

        NA_SkillExplosion bulletExplosion;
        bulletExplosion = poolManager.SpawnObj(NAExplosionPrefab, transform.position, PoolType.FungusExplosion);
        bulletExplosion.GetParticleGradient(gradientParticle, gradientBullet);
    }

    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);
        MoveToTarget();
    }
}
