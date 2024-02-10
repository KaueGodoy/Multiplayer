using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _quickJoinButton;

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        _createLobbyButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.CreateLobby("My lobby", false);
        });
        _quickJoinButton.onClick.AddListener(() =>
        {
            KitchenGameLobby.Instance.QuickJoin();
        });
    }
}
