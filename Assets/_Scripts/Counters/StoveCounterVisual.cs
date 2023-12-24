using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    [SerializeField] private GameObject _stoveOnGameObject;
    [SerializeField] private GameObject _particlesGameObject;

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
        _stoveOnGameObject.SetActive(showVisual);
        _particlesGameObject.SetActive(showVisual);
    }
}
