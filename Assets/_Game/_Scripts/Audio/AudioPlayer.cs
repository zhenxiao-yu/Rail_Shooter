using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance {get; private set;}

    [SerializeField] AudioLibrary audioLib;
    [SerializeField] int audioSourcesNumber = 6;
    [SerializeField] AudioMixerGroup sfxGroup, bgmGroup;

    //FIFO Data structure
    private Queue<AudioSource> audioSources = new Queue<AudioSource>();
    private AudioSource bgmSource;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //run init method
        Init();
    }
    
    void Init()
    {
        GameObject bgmObject = new GameObject("BGM Source");
        bgmSource = bgmObject.AddComponent<AudioSource>();
        bgmSource.spatialBlend = 0f;
        bgmSource.outputAudioMixerGroup = bgmGroup;
        bgmObject.transform.SetParent(transform);

        for (int i = 0; i < audioSourcesNumber; i++)
        {
            GameObject sfxObject = new GameObject("SFX Source " + (i + 1).ToString("00"));
            AudioSource temp = sfxObject.AddComponent<AudioSource>();
            temp.spatialBlend = 1f;
            temp.outputAudioMixerGroup = sfxGroup;
            sfxObject.transform.SetParent(transform);
            audioSources.Enqueue(temp);
        }
    }

    public void PlaySFX(string audioName, Transform audioLocation )
    {
        AudioSource temp = audioSources.Dequeue();
        temp.transform.position = audioLocation.position;
        temp.PlayAudioData(audioLib.GetAudioByName(audioName));
        audioSources.Enqueue(temp);
    }
}
