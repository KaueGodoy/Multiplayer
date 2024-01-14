using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;

    private Animator _animator;

    private const string _isFlashing = "IsFlashing";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        bool show = _stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;

        _animator.SetBool(_isFlashing, show);
    }

}
