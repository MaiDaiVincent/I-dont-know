using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class BossSlot : MonoBehaviour
{
    public bool isSelecting;
    public bool isShowingInfo;

    [SerializeField] private Button selectBossButton;
    [SerializeField] private Button showInfoButton;
    [SerializeField] private Button infoPanelButton;

    [SerializeField] private Image elementalImage;
    [SerializeField] private Image avatarIcon;
    [SerializeField] private Image bgOutlineImage;

    [SerializeField] private TextMeshProUGUI lvText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] float initPosY;
    [SerializeField] private RectTransform rectTransformRoot;
    public BossPackedConfig BossPackedConfig { get => bossPackedConfig; }
    [SerializeField] private BossPackedConfig bossPackedConfig;

    private HomeUI homeUI;

    private Tween scaleTween;
    private Tween moveYTween;
    private void Awake()
    {
        homeUI = transform.root.GetComponent<HomeUI>();
        SetBgOutlineColor(homeUI.GetColor(BgColorOutlineType.normal));

        selectBossButton.onClick.AddListener(OnSelectBossClick);
        showInfoButton.onClick.AddListener(OnShowInfoClick);
        infoPanelButton.onClick.AddListener(OnInfoPanelClick);

        SetShowingInfoState(false);

        initPosY = rectTransformRoot.localPosition.y;
    }
    private void OnDestroy()
    {
        scaleTween.Kill();
        moveYTween.Kill();
    }
    private void OnSelectBossClick()
    {
        AudioManager.Instance.PlayOnClickButton();

        EventManager.ActionOnSelectBoss(this);
    }

    private void OnShowInfoClick()
    {
        isShowingInfo = !isShowingInfo;
        EventManager.ActionOnShowInfoBoss(this, isShowingInfo);
    }
    private void OnInfoPanelClick()
    {
        SetShowingInfoState(false);
    }
    public void SetShowingInfoState(bool state)
    {
        isShowingInfo = state;
        infoPanelButton.gameObject.SetActive(state);
    }
    public void SetBossPackedConfig(BossPackedConfig config) => bossPackedConfig = config;
    public void SetElementalImage(Sprite sprite) => elementalImage.sprite = sprite;
    public void SetAvatarImage(Sprite sprite) => avatarIcon.sprite = sprite;


    public void SetLv(int value) => lvText.text = "Lv. " + value.ToString();
    public void SetHealth(float value) => healthText.text = "Health: " + value.ToString();
    public void SetName(string name) => nameText.text = "Name: " + name;
    public void SetBgOutlineColor(Color color) => bgOutlineImage.color = color;

    public void SetSelectState(bool state)
    {
        isSelecting = state;

        if (state) DOSelect(1.05f, initPosY + 30f, bossPackedConfig.config.bossColor);
        else DOSelect(1f, initPosY, homeUI.GetColor(BgColorOutlineType.normal));
    }

    void DOSelect(float scaleValue, float moveYValue, Color color)
    {
        scaleTween = transform.DOScale(scaleValue, GameConfig.scaleBtnDuration);

        moveYTween = rectTransformRoot.DOAnchorPosY(moveYValue, GameConfig.moveYBossSlotDuration);

        SetBgOutlineColor(color);

    }
    public void OnPointerExit()
    {
        if (!isSelecting)
        {
            transform.DOScale(1f, GameConfig.scaleCardDuration);
            SetBgOutlineColor(homeUI.GetColor(BgColorOutlineType.normal));
        }
    }

    public void OnPointerEnter()
    {
        if (!isSelecting)
        {
            transform.DOScale(1.02f, GameConfig.scaleCardDuration);
            SetBgOutlineColor(homeUI.GetColor(BgColorOutlineType.highlighted));
        }
    }

    public void OnPointerDown()
    {
        if (!isSelecting)
        {
            transform.DOScale(0.98f, GameConfig.scaleCardDuration);
        }
    }
}
