using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button _soundEffectsButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _closeButton;
    [Space]
    [SerializeField] private TextMeshProUGUI _soundEffectsText;
    [SerializeField] private TextMeshProUGUI _musicText;
    [Space]
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAltText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    [Space]
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interactAltButton;
    [SerializeField] private Button _pauseButton;
    [Space]
    [SerializeField] private Transform _pressToRebindKeyTransform;
    [Space]
    [SerializeField] private TextMeshProUGUI _gamePadInteractText;
    [SerializeField] private TextMeshProUGUI _gamePadInteractAltText;
    [SerializeField] private TextMeshProUGUI _gamePadPauseText;
    [Space]
    [SerializeField] private Button _gamePadInteractButton;
    [SerializeField] private Button _gamePadInteractAltButton;
    [SerializeField] private Button _gamePadPauseButton;

    private Action onCloseButtonAction;

    private void Awake()
    {
        Instance = this;

        _soundEffectsButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        _closeButton.onClick.AddListener(() =>
        {
            Hide();
            onCloseButtonAction();
        });

        _moveUpButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Up); });
        _moveDownButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Down); });
        _moveLeftButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Left); });
        _moveRightButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Move_Right); });
        _interactButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Interact); });
        _interactAltButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.InteractAlternate); });
        _pauseButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.Pause); });

        _gamePadInteractButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.GamePad_Interact); });
        _gamePadInteractAltButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.GamePad_InteractAlternate); });
        _gamePadPauseButton.onClick.AddListener(() => { RebindBiding(GameInput.Binding.GamePad_Pause); });
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnLocalGameUnpaused += KitchenGameManager_OnGameUnpaused;

        UpdateVisual();

        Hide();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        _soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        _musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        _moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        _moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        _moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        _moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        _interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        _pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        _gamePadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
        _gamePadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_InteractAlternate);
        _gamePadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);

        _soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        _pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBiding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
