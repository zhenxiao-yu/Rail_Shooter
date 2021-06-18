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
        bgmSource.loop = true;
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

    public void PlaySFX(AudioGetter audioSfx, Transform audioLocation = null )
    {
        AudioSource temp = audioSources.Dequeue();

        if(audioLocation != null)
        {
            temp.transform.position = audioLocation.position;
            temp.spatialBlend = 1f; //3d sound
        }
        else
        {
            temp.spatialBlend = 0f; //2d sound
        }
        temp.PlayAudioData(audioLib.GetAudioByName(audioSfx.AudioName));
        audioSources.Enqueue(temp);
    }

    public void PlayMusic(AudioGetter music)
    {
        bgmSource.PlayAudioData(audioLib.GetAudioByName(music.AudioName));
        bgmSource.pitch = 1f;
    }
}
