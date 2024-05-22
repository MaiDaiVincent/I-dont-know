using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using Color = UnityEngine.Color;

public class TextPopUp : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private float scale = 0f;
    public TextMeshPro popUpText;
    private Sequence sequence;
    private Tween scaleTween;
    private void OnEnable()
    {
        float posX = Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f);
        float posY = Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f);

        transform.position = new Vector2(posX, posY);
        transform.localScale = Vector3.one;
        popUpText.alpha = 1f;
        popUpText.color = Color.white;


        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + 1f, duration));
        sequence.Append(popUpText.DOFade(0, duration));
        sequence.OnComplete(HideObj);
    }
    public void SetPopUpDamage(int value, bool canCrit, Color color)
    {
        if (canCrit) scale = GameConfig.popUpDamageScaleCrit;
        else scale = GameConfig.popUpDamageScaleNotCrit;

        scaleTween = transform.DOScale(scale, duration);
        popUpText.text = value.ToString();
        popUpText.color = color;
    }
    void HideObj()
    {
        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        sequence.Kill();
        scaleTween.Kill();
    }
}
