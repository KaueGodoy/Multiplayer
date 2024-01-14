using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [Space]
    [SerializeField] private TextMeshProUGUI _messageText;
    [Space]
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _failedColor;
    [Space]
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _failedSprire;

    //private string _deliveredSuccess = "DELIVERY\nSUCCESS";
    //private string _deliveredFailed = "DELIVERY\nFAILED";

    private const string _popup = "Popup";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(_popup);
        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = "DELIVERY\nSUCCESS";

        //ShowDeliveryResult(_successColor, _successSprite, _deliveredSuccess);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(_popup);
        _backgroundImage.color = _failedColor;
        _iconImage.sprite = _failedSprire;
        _messageText.text = "DELIVERY\nFAILED";
    }

    private void ShowDeliveryResult(Color color, Sprite sprite, string message)
    {
        _backgroundImage.color = color;
        _iconImage.sprite = sprite;
        _messageText.text = message;
    }

}
