using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHydroEB_Skill : EB_Skill
{
    public ParticleSystem effect;

    private CircleCollider2D circleCollider2D;
    private SpriteRenderer redZone;
    private Tween scaleTween;
    protected override void Awake()
    {
        base.Awake();
        redZone = GetComponentInChildren<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        circleCollider2D.enabled = false;
        redZone.gameObject.SetActive(true);
        effect.gameObject.SetActive(false);
    }
    void OnDestroy()
    {
        scaleTween.Kill();
    }
    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        Target = target;
        Direction = direction;

        if (Target != null) transform.position = Target.position;

        float range = SkillConfig.range;
        Vector3 scale = new Vector3(range, range, range);
        scaleTween = transform.DOScale(scale, 1f).OnComplete(() =>
        {
            redZone.gameObject.SetActive(false);
            circleCollider2D.enabled = true;
            effect.gameObject.SetActive(true);

            if (disableCoroutine != null) StopCoroutine(disableCoroutine);
            disableCoroutine = StartCoroutine(DisableCoroutine());
        });
    }
    protected override IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    protected override void EnterDetection(GameObject obj)
    {
        if (obj.GetComponent<HealthBase>())
        {
            CauseDamage(obj);
        }
    }
}
