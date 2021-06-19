using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class VolumeSettings
{
    [SerializeField] GameObject panel;
    [SerializeField] Slider sfxSlider, bgmSlider;
    [SerializeField] AudioMixer mainMixer;

    private string sfxKey = "SfxKey" , bgmKey = "BgmKey";
    public GameObject Panel { get => panel; }

    public void Init()
    {
        AdjustSfx(PlayerPrefs.GetFloat(sfxKey, 0f));
        AdjustBgm(PlayerPrefs.GetFloat(bgmKey, 0f));

        sfxSlider.value = PlayerPrefs.GetFloat(sfxKey, 0f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmKey, 0f);


        sfxSlider.onValueChanged.AddListener(AdjustSfx);
        bgmSlider.onValueChanged.AddListener(AdjustBgm);
    }

    void AdjustSfx(float value)
    {
        mainMixer.SetFloat("SfxVolume", value);
        PlayerPrefs.SetFloat(sfxKey, value);
        PlayerPrefs.Save();
    }
    void AdjustBgm(float value)
    {
        mainMixer.SetFloat("BgmVolume", value);
        PlayerPrefs.SetFloat(bgmKey, value);
        PlayerPrefs.Save();
    }
}
