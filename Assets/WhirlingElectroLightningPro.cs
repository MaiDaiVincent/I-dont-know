using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingElectroLightningPro : MonoBehaviour
{
    public FungusInfoReader FungusInfo { get; set; }
    public SkillConfig SkillConfig { get; set; }
    private TriggerDetection triggerDetection;
    private BoxCollider2D boxCollider2d;
    private SpriteRenderer spriteRenderer;
    private Coroutine hideCoroutine;
    private PoolManager poolManager => PoolManager.Instance;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        triggerDetection = GetComponent<TriggerDetection>();

        triggerDetection.detectionEnterEvent.AddListener((GameObject obj) => TriggerDetection(obj));
    }
    private void OnEnable()
    {
        boxCollider2d.enabled = true;

        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideCoroutine());
    }
    protected IEnumerator HideCoroutine()
    {
        float timer = 1;
        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1);
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, timer);
            
            yield return null;
        }
        gameObject.SetActive(false);
    }
    //anim event
    public void BoxColliderDisable()
    {
        if (boxCollider2d.enabled) boxCollider2d.enabled = false;
    }
    public void EndEffect()
    {
    }
    void TriggerDetection(GameObject obj)
    {
        if (obj.GetComponent<HealthBase>())
        {
            FungusData data = FungusInfo.FungusData;
            bool canCrit = Helper.CanCrit(data.critRate);
            int critDamage = Helper.CritDamage(data.atk, data.critDamagePercent);
            int damage = Helper.CauseDamage(data.atk, canCrit, critDamage, SkillConfig.valuePercent);
            obj.GetComponent<HealthBase>().TakeDamage(damage);

            Color color = FungusInfo.FungusData.fungusConfig.fungusColor;
            Vector2 collisionPos = obj.transform.position;

            TextPopUp textPopUp;
            textPopUp = poolManager.SpawnObj(poolManager.GetTextPopUp(), collisionPos, PoolType.TextPopUp);
            textPopUp.SetPopUpDamage(damage, canCrit, color);

            BoxColliderDisable();
        }
    }
}
