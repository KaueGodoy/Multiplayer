using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _quickJoinButton;

    [SerializeField] private Button _joinCodeButton;
    [SerializeField] private TMP_InputField _joinCodeInputField;

    [SerializeField] private LobbyCreateUI _lobbyCreateUI;

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        _createLobbyButton.onClick.AddListener(() =>
        {
            _lobbyCreateUI.Show();
        });
        _quickJoinButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.QuickJoin();
        });
        _joinCodeButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.JoinWithCode(_joinCodeInputField.text);
        });
    }
}
