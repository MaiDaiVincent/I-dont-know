using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tạo vùng gây sát thương theo thời gian
public class WhirlingCyroEB_Skill : EB_Skill
{
    private PolygonCollider2D polygonCollider2D;
    private Tween scaleTween;
    protected override void Awake()
    {
        triggerDetection = GetComponentInChildren<TriggerDetection>();
        polygonCollider2D = GetComponentInChildren<PolygonCollider2D>();

        _ListenEvents();
    }
    void OnEnable()
    {
        if (polygonCollider2D == null) return;
     
        transform.localScale = Vector3.zero;
        polygonCollider2D.enabled = false;

        StartCoroutine(CollisionCoroutine());
    }
    IEnumerator CollisionCoroutine()
    {
        while (true)
        {
            if (!polygonCollider2D.enabled)
            {
                Debug.Log("enable");
                polygonCollider2D.enabled = true;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    void OnDestroy()
    {
        scaleTween.Kill();
        EventManager.onSwitchFungus -= OnSwitchFungus;

    }
    private void Update()
    {
        if (Target == null) return;

        transform.position = Target.position;

    }
    protected override void _ListenEvents()
    {
        base._ListenEvents();
        EventManager.onSwitchFungus += OnSwitchFungus;
    }
    public void OnSwitchFungus(FungusInfoReader info, FungusCurrentStatusHUD CurrentStatusHUD)
    {
        Target = info.transform;
    }
    protected override void StayDetection(GameObject obj)
    {
        if (obj.GetComponent<HealthBase>())
        {
            CauseDamage(obj);
            polygonCollider2D.enabled = false;

        }
    }

    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);

        if (Target != null) transform.position = Target.position;

        float range = SkillConfig.range;
        Vector3 scale = new Vector3(range, range, range);
        scaleTween = transform.DOScale(scale, 0.5f).OnComplete(() =>
        {
            polygonCollider2D.enabled = true;
        });
    }
    protected override void EnterDetection(GameObject @object)
    {
        //không kiểm tra khi va chạm từ bên ngoài vào
    }
    protected override IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(SkillConfig.activeTime);
        gameObject.SetActive(false);
    }
}
