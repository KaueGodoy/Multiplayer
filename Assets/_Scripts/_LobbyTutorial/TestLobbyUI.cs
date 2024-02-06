using UnityEngine;
using UnityEngine.UI;

public class TestLobbyUI : MonoBehaviour
{
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private TestLobby _testLobby;

    private void Awake()
    {
        _createLobbyButton.onClick.AddListener(() =>
        {
            _testLobby.CreateLobby();
        });
    }
}
