using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInfoReader : MonoBehaviour
{
    [SerializeField] private SpriteRenderer model;


    public BossCurrentStatusHUD currentStatusHUD;
    public BossData BossData { get; private set; }

    public BossController BossController { get; private set; }

    private void Awake()
    {
        BossController = GetComponent<BossController>();
    }
    public void GetData(BossData bossData)
    {
        BossData = bossData;

        GetModel(bossData.bossConfig.bossModelSprite);
    }

    public void GetCurrentStatusHUD(BossCurrentStatusHUD statusHUD)
    {
        currentStatusHUD = statusHUD;
    }

    void GetModel(Sprite sprite) => model.sprite = sprite;
}
