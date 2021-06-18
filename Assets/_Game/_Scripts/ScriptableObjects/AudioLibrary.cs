using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "AudioLib", menuName = "Audio Library")]
public class AudioLibrary : ScriptableObject
{
    [SerializeField] AudioData[] audioList;

    public static List<string> audioNamesList = new List<string>();

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

    void OnValidate()
    {
        audioNamesList.Clear();

        foreach (var audio in audioList)
        {
            audioNamesList.Add(audio.AudioName);
        }
    }
}

[System.Serializable]
public class AudioGetter
{
    public string AudioName { get => AudioLibrary.audioNamesList[id]; }
    public int id;
}
