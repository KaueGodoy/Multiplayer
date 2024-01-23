using UnityEditor.Rendering;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private const string PlayerPrefs_SoundEffectsVolume = "SoundEffectsVolume";

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float _volume = .5f;

    private void Awake()
    {
        Instance = this;

        _volume = PlayerPrefs.GetFloat(PlayerPrefs_SoundEffectsVolume, .5f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;

        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

        Player.OnAnyPickedSomething += Player_OnPickedSomething;

        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;

        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.Trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.ObjectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        Player player = sender as Player;
        PlaySound(audioClipRefsSO.ObjectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        Debug.Log(transform.position);
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.DeliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.DeliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * _volume);
    }

    public void PlayFootstepsSounds(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.Footstep, position, volume);
    }

    public void PlayCountdownSound()
    {
        PlaySound(audioClipRefsSO.Warning[0], Vector3.zero);
    }

    public void PlayWarningSound(Vector3 position)
    {
        PlaySound(audioClipRefsSO.Warning[1], position);
    }

    public void ChangeVolume()
    {
        _volume += .1f;
        if (_volume > 1f)
            _volume = 0f;

        PlayerPrefs.SetFloat(PlayerPrefs_SoundEffectsVolume, _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() { return _volume; }
}
