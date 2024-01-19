using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{

    [SerializeField] private Button _serverBtn;
    [SerializeField] private Button _hostBtn;
    [SerializeField] private Button _clientBtn;


    private void Awake()
    {
        _serverBtn.onClick.AddListener(() =>
        {
            Debug.Log("SERVER");
            NetworkManager.Singleton.StartServer();
            Hide();
        });
        _hostBtn.onClick.AddListener(() =>
        {
            Debug.Log("HOST");
            NetworkManager.Singleton.StartHost();
            Hide();
        });
        _clientBtn.onClick.AddListener(() =>
        {
            Debug.Log("CLIENT");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }
}