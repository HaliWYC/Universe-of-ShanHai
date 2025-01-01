using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void SetSound(SoundDetails soundDetails)
    {
        if (soundDetails == null) return;
        audioSource.clip = soundDetails.audioClip;
        audioSource.volume = soundDetails.audioVolume;
        audioSource.pitch = Random.Range(soundDetails.audioPitchMin, soundDetails.audioPitchMax);
        audioSource.Play();
    }
}
