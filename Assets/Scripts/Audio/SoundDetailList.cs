using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundDetailList", menuName = "Sound/SoundDetailList")]
public class SoundDetailList : ScriptableObject
{
    public List<SoundDetails> soundDetailsList;
    public SoundDetails GetSoundDetails(string soundName)
    {
        return soundDetailsList.Find(x => x.soundName == soundName);
    }
}

[System.Serializable]
public class SoundDetails
{
    public string soundName;
    public AudioClip audioClip;
    [Range(0.1f, 1.5f)]
    public float audioPitchMin;

    [Range(0.1f, 1.5f)]
    public float audioPitchMax;

    [Range(0.1f, 1f)]
    public float audioVolume;
}
