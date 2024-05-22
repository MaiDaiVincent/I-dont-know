using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingElectroLightning : MonoBehaviour
{
    public FungusInfoReader FungusInfo { get; set; }
    public SkillConfig SkillConfig { get; set; }
    private TriggerDetection triggerDetection;
    private BoxCollider2D boxCollider2d;
    private PoolManager poolManager => PoolManager.Instance;
    private void Awake()
    {
        boxCollider2d = GetComponent<BoxCollider2D>();
        triggerDetection = GetComponent<TriggerDetection>();
        
        triggerDetection.detectionEnterEvent.AddListener((GameObject obj) => TriggerDetection(obj));
    }
    private void OnEnable()
    {
        boxCollider2d.enabled = true;
    }
    //anim event
    public void BoxColliderDisable()
    {
        if(boxCollider2d.enabled) boxCollider2d.enabled = false;
    }
    public void EndEffect()
    {
        gameObject.SetActive(false);
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


            BoxColliderDisable();
            
            Color color = FungusInfo.FungusData.fungusConfig.fungusColor;
            Vector2 collisionPos = obj.transform.position;

            TextPopUp textPopUp;
            textPopUp = poolManager.SpawnObj(poolManager.GetTextPopUp(), collisionPos, PoolType.TextPopUp);
            textPopUp.SetPopUpDamage(damage, canCrit, color);

        }
    }
}
