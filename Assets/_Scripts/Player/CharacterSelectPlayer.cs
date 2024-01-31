using UnityEngine;

public class CharacterSelectPlayer : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
