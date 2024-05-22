using System;
using UnityEditor.Animations;
using UnityEngine;

public class FungusInfoReader : MonoBehaviour
{

    [SerializeField] private SpriteRenderer model;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem switchFungusEffect;
    [SerializeField] private ParticleSystem shadowSwitchFungusEffect;

    public FungusData FungusData;

    [Header("HUD")]
    public FungusCurrentStatusHUD fungusCurrentStatusHUD;
    public FungusSlotHUD fungusSlotHUD;
    public FungusController FungusController { get; private set; }

    private void Awake()
    {
        FungusController = GetComponent<FungusController>();
    }

    public void GetData(FungusData data)
    {
        FungusData = data;

        GetModel(FungusData.fungusConfig.fungusModelSprite, FungusData.fungusConfig.dissolveMaterial);

        GetAnimatorController(this.FungusData.fungusConfig.animatorController);

        GetParticleGradient(this.FungusData.fungusConfig.gradientParticle);

        switchFungusEffect.Play();
    }



    void GetParticleGradient(Gradient gradient)
    {
        var switchColor = switchFungusEffect.colorOverLifetime;
        switchColor.color = gradient;

        var shadowSwitchColor = shadowSwitchFungusEffect.colorOverLifetime;
        shadowSwitchColor.color = gradient;
    }
    void GetModel(Sprite sprite, Material material)
    {
        model.sprite = sprite;
        model.material = material;
    }
    void GetAnimatorController(AnimatorController animatorController)
    {
        animator.runtimeAnimatorController = animatorController;
    }
}
