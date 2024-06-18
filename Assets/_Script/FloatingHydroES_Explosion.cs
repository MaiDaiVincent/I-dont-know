using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingHydroES_Explosion : MonoBehaviour
{
    public int Damage { get; private set; }
    public bool CanCrit { get; set; } = false;
    public Color PopUpColor { get; set; }

    public SkillConfig SkillConfig { get; private set; }
    public FungusInfoReader fungusInfo { get; private set; }

    private TriggerDetection triggerDetection;
    private PoolManager poolManager => PoolManager.Instance;

    private Coroutine disableCoroutine;

    public void Awake()
    {
        triggerDetection = GetComponent<TriggerDetection>();
        triggerDetection.detectionEnterEvent.AddListener((GameObject obj) => TriggerDetection(obj));
    }
    public void GetInfo(int damage, bool canCrit, Color popUpColor)
    {
        Damage = damage;
        CanCrit = canCrit;
        PopUpColor = popUpColor;
    }
    private void OnEnable()
    {
        if (disableCoroutine != null) StopCoroutine(disableCoroutine);
        disableCoroutine = StartCoroutine(DisableCoroutine());
    }
    IEnumerator DisableCoroutine()
    {
        AudioManager.Instance.PlayExplosion();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
  
    public void TriggerDetection(GameObject @object)
    {
        if (@object.GetComponent<HealthBase>())
        {
            Vector3 collisionPos = @object.transform.position;

            int damage = Damage / 2;
            @object.GetComponent<HealthBase>().TakeDamage(damage);

            TextPopUp textPopUp;
            textPopUp = poolManager.SpawnObj(poolManager.GetTextPopUp(), collisionPos, PoolType.TextPopUp);
            textPopUp.SetPopUpDamage(damage, CanCrit, PopUpColor);
        }

    }
}
