using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter _stoveCounter;
    private AudioSource _audioSource;


    private bool _playWarningSound;
    private float _warningSoundTimer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _stoveCounter.OnStateChanged += _stoveCounter_OnStateChanged;
        _stoveCounter.OnProgressChanged += _stoveCounter_OnProgressChanged;
    }

    private void _stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = 0.5f;
        _playWarningSound = _stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void _stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playsound = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
        if (playsound)
            _audioSource.Play();
        else
            _audioSource.Pause();
    }

    private void Update()
    {
        if (_playWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = .2f;
                _warningSoundTimer = warningSoundTimerMax;

                SoundManager.Instance.PlayWarningSound(_stoveCounter.transform.position);
            }
        }
    }
}
