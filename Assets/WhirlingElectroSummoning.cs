using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlingElectroSummoning : MonoBehaviour
{

    public FungusInfoReader fungusInfo { get; set; }
    public SkillConfig skillConfig { get; set; }

    [SerializeField] private WhirlingElectroLightning whirlingElectroLightningPrefab;
    private WhirlingElectroLightning whirlingElectroLightning;
    private PoolType poolType = PoolType.WhirlingElectroLightning;

    private Transform target;
    private TargetDetector targetDetector;
    private Coroutine attackCoroutine;
    private PoolManager poolManager => PoolManager.Instance;
    private void Awake()
    {
        targetDetector = GetComponentInChildren<TargetDetector>();

    }
    private void OnEnable()
    {
        if (attackCoroutine != null) StopCoroutine(AttackCoroutine());
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            target = targetDetector.Target();
            if (target == null) StopCoroutine(AttackCoroutine());

            whirlingElectroLightning = poolManager.SpawnObj(whirlingElectroLightningPrefab, target.position, poolType);

            if (skillConfig != null) whirlingElectroLightning.SkillConfig = skillConfig;
            if (fungusInfo != null) whirlingElectroLightning.FungusInfo = fungusInfo;

        }


    }
}
