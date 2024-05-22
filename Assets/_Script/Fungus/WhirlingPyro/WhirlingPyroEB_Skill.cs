using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//nhảy đáp đất vào mục tiêu
public class WhirlingPyroEB_Skill : EB_Skill
{
    public float knockbackForce = 30;

    private Tween moveUpYTween;
    private Tween moveDownYTween;
        
    private FungusController fungusController;


    public override void ShowcaseSkill(Transform target, Vector2 direction)
    {
        base.ShowcaseSkill(target, direction);

        fungusController = fungusInfo.FungusController;

        moveUpYTween = fungusInfo.transform.DOMoveY(20f, 2f).SetEase(Ease.InOutQuart);

        moveUpYTween.OnStart(() =>
        {
            cameraController.Target = null;

            fungusController.capsuleCollider2D.enabled = false;

            target.GetComponent<BossAttack>().canAtk = false;

            fungusController.EB_State(true);
        });

        moveUpYTween.OnComplete(() =>
        {
            cameraController.Target = Target;
            fungusInfo.transform.position = new Vector2(target.position.x, fungusInfo.transform.position.y);
            moveDownYTween = fungusInfo.transform.DOMoveY(target.position.y, 1f).SetEase(Ease.InBack);

            moveDownYTween.OnComplete(() =>
            {
                fungusController.capsuleCollider2D.enabled = true;

                CauseDamage(target.gameObject);

                cameraController.Target = fungusInfo.transform;

                if (target.GetComponent<BossController>())
                {
                    target.GetComponent<BossController>().rb2d.AddForce(knockbackForce * direction, ForceMode2D.Impulse);
                }

                fungusController.EB_State(false);

                gameObject.SetActive(false);
            });
        });

    }
}
