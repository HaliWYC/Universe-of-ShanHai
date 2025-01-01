using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio")]

    public SoundDetailList soundDetailList;
    public SceneSoundList sceneSoundList;

    [Header("Audio Source")]

    public AudioSource gameSource;
    public AudioSource ambientSource;

    private Coroutine SoundCoroutine;

    [Header("Audio Mixer")]

    public AudioMixer audioMixer;

    [Header("Snapshots")]

    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot muteSnapshot;

    private float waitingTime;

    public void AfterRoomLoad(object obj)
    {
        Room room = obj as Room;
        SceneSound sceneSound = sceneSoundList.GetSceneSound(room.roomData.roomType);
        if (sceneSound == null)
        {
            return;
        }
        SoundDetails ambientSound = soundDetailList.GetSoundDetails(sceneSound.ambientSound);
        SoundDetails gameSound = soundDetailList.GetSoundDetails(sceneSound.gameSound);

        if (SoundCoroutine != null)
        {
            StopCoroutine(SoundCoroutine);
        }
        SoundCoroutine = StartCoroutine(PlaySoundClip(gameSound, ambientSound));
    }

    private IEnumerator PlaySoundClip(SoundDetails gameSound, SoundDetails ambientSound)
    {
        if (gameSound == null || (gameSound == null && ambientSound == null)) yield break;
        PlayAmbientSoundClip(ambientSound);
        waitingTime = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(waitingTime);
        PlayGameSoundClip(gameSound);
    }

    /// <summary>
    /// Play the game BGM
    /// </summary>
    /// <param name="soundDetails"></param>
    public void PlayGameSoundClip(SoundDetails soundDetails)
    {
        audioMixer.SetFloat("GameVolume", ConvertSoundVolume(soundDetails.audioVolume));
        gameSource.clip = soundDetails.audioClip;
        if (gameSource.isActiveAndEnabled)
        {
            gameSource.Play();
        }
        normalSnapshot.TransitionTo(8f);
    }
    /// <summary>
    /// Play the ambient BGM
    /// </summary>
    /// <param name="soundDetails"></param>
    private void PlayAmbientSoundClip(SoundDetails soundDetails)
    {
        if (soundDetails == null) return;
        audioMixer.SetFloat("AmbientVolume", ConvertSoundVolume(soundDetails.audioVolume));
        ambientSource.clip = soundDetails.audioClip;
        if (ambientSource.isActiveAndEnabled)
        {
            ambientSource.Play();
        }
    }

    private float ConvertSoundVolume(float volume)
    {
        return (volume * 100 - 80);
    }
}
