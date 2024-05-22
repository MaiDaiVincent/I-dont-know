using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FungusAniEvent : MonoBehaviour
{
    //đảm bảo chỉ gọi 1 nhánh trong blend tree
    [SerializeField] private bool isNA_ingEvent;
    [SerializeField] private bool isES_ingEvent;
    [SerializeField] private bool isEB_ingEvent;


    public UnityEvent OnStartNA_SkillEvent;
    public UnityEvent OnEndNA_SkillEvent;

    public UnityEvent OnStartES_SkillEvent;
    public UnityEvent OnEndES_SkillEvent;

    public UnityEvent OnStartEB_SkillEvent;
    public UnityEvent OnEndEB_SkillEvent;

    private void Awake()
    {

    }
    public void OnStartAttack()
    {
        OnStartNA_SkillEvent?.Invoke();
        isNA_ingEvent = true;
    }

    public void OnEndAttack()
    {
        if (!isNA_ingEvent) return;

        OnEndNA_SkillEvent?.Invoke();
        isNA_ingEvent = false;
    }

    public void OnStartES_Skill()
    {
        OnStartES_SkillEvent?.Invoke();
        isES_ingEvent = true;
        isNA_ingEvent = false;

    }
    public void OnEndES_Skill()
    {
        if (!isES_ingEvent) return;

        OnEndES_SkillEvent?.Invoke();
        isES_ingEvent = false;

    }

    public void OnStartEB_Skill()
    {
        OnStartEB_SkillEvent?.Invoke();
        isEB_ingEvent = true;
        isNA_ingEvent = false;

    }
    public void OnEndEB_Skill()
    {
        if (!isEB_ingEvent) return;

        OnEndEB_SkillEvent?.Invoke();
        isEB_ingEvent = false;

    }
}
