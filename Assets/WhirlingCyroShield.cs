using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventManager;

public class WhirlingCyroShield : HealthBase
{
    public float activeTime { get; set; }
    public int Health { get; set; }
    public Transform Target { get; set; }

    private void Awake()
    {
        EventManager.onSwitchFungus += OnSwitchFungus;
        OnDiedEvent += OnDied;
    }
    void OnDestroy()
    {
        EventManager.onSwitchFungus -= OnSwitchFungus;
        OnDiedEvent -= OnDied;
    }
    private void Update()
    {
        if (Target == null) return;
        transform.position = Target.position;
    }
    public void OnSwitchFungus(FungusInfoReader info, FungusCurrentStatusHUD CurrentStatusHUD)
    {
        Target = info.transform;
    }
    public override void TakeDamage(int value)
    {
        int damage = value;

        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            OnDiedEvent?.Invoke();
        }
    }
    public void OnDied()
    {
        gameObject.SetActive(false);
        FungusManager.Instance.isHaveShield = false;
    }

}
