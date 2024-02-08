using UnityEngine;
using UnityEngine.UI;

public class TestLobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _searchLobbyButton;
    [SerializeField] private Button _joinLobbyButton;
    [SerializeField] private Button _quickJoinLobbyButton;

    [SerializeField] private TestLobby _testLobby;

    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(() =>
        {
            _testLobby.CreateLobby();
        });
        _searchLobbyButton.onClick.AddListener(() =>
        {
            _testLobby.ListLobbies();
        });
        _joinLobbyButton.onClick.AddListener(() =>
        {
            _testLobby.JoinLobby();
        });
        _quickJoinLobbyButton.onClick.AddListener(() =>
        {
            _testLobby.QuickJoinLobby();
        });
    }
}
