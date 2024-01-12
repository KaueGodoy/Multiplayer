using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PlayerPrefs_MusicVolume = "MusicVolume";

    private AudioSource _audioSource;

    private float _volume = .3f;

    private void Awake()
    {
        Instance = this;

        _audioSource = GetComponent<AudioSource>();

        _volume = PlayerPrefs.GetFloat(PlayerPrefs_MusicVolume, .3f);
        _audioSource.volume = _volume;
    }

    public void ChangeVolume()
    {
        _volume += .1f;
        if (_volume > 1f)
            _volume = 0f;

        _audioSource.volume = _volume;

        PlayerPrefs.SetFloat(PlayerPrefs_MusicVolume, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() { return _volume; }
}
