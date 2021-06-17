using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioData
{
    [SerializeField] string audioName;
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float minPitch = 0.9f, maxPitch = 1.1f;

    public string AudioName { get => audioName; }
    public AudioClip GetAudioClip { get => audioClips[Random.Range(0, audioClips.Length)];} //can define more than one type of sound
    //Get a random pitch between min and max value
    public float GetPitch { get => Random.Range(minPitch, maxPitch); }
}
