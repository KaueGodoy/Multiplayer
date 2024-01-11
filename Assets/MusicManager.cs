using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource _audioSource;

    private float _volume = .3f;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume()
    {
        _volume += .1f;
        if (_volume > 1f)
            _volume = 0f;

        _audioSource.volume = _volume;
    }

    public float GetVolume() { return _volume; }
}
