using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AudioLib", menuName = "Audio Library")]
public class AudioLiberary : ScriptableObject
{
    [SerializeField] AudioData[] audioList;

    public AudioData GetAudioByName(string name)
    {
        AudioData value = null;

        foreach (var audio in audioList)
        {
            if (audio.AudioName == name)
                value = audio;
        }

        return value;
    }
}
    
