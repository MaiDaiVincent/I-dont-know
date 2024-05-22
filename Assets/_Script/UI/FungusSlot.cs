using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FungusSlot : MonoBehaviour
{
    public int slotIndex;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Image avatarImage;
    [SerializeField] private Button unPick;
    public FungusPackedConfig FungusPackedConfig { get => fungusPackedConfig; }
    private FungusPackedConfig fungusPackedConfig;
    private void Awake()
    {

        SetAvatar(defaultSprite);

        unPick.onClick.AddListener(() => EventManager.ActionOnUnPickFungus(slotIndex, fungusPackedConfig));

        EventManager.onPickFungus += OnPickFungus;
        EventManager.onUnPickFungus += OnUnPickFungus;
    }

    public void OnUnPickFungus(int index, FungusPackedConfig config)
    {
        if (fungusPackedConfig != config || fungusPackedConfig == null) return;
        ResetConfig();
    }

    private void OnDestroy()
    {
        EventManager.onPickFungus -= OnPickFungus;
        EventManager.onUnPickFungus -= OnUnPickFungus;

    }
    private void OnPickFungus(int index, FungusPackedConfig config)
    {
        if (slotIndex != index) return;

        fungusPackedConfig = config;
        LoadConfig();
    }
    public void LoadConfig()
    {
        if(fungusPackedConfig != null)
        {
            SetAvatar(fungusPackedConfig.config.fungusAvatar);
        }
    }
    public void ResetConfig()
    {
        fungusPackedConfig = null;
        SetAvatar(defaultSprite);
    }
    public void OnPointerExit()
    {
        DOScaleSlot(1, GameConfig.scaleCardDuration);
    }

    public void OnPointerEnter()
    {
        DOScaleSlot(1.05f, GameConfig.scaleCardDuration);
    }

    public void OnPointerDown()
    {
        DOScaleSlot(0.95f, GameConfig.scaleCardDuration);
    }
    public void OnPointerUp()
    {
        DOScaleSlot(1, GameConfig.scaleCardDuration);
    }

    public void DOScaleSlot(float value, float duration)
    {
        if (fungusPackedConfig == null) return;
        transform.DOScale(value, duration);
    }
    public void SetAvatar(Sprite avatar) => avatarImage.sprite = avatar;
    public void GetFungusPackedConfig(FungusPackedConfig config) => fungusPackedConfig = config;
}
