using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    private float masterVolume = 0.7f;
    private float musicVolume = 0.5f;
    private float sfxVolume = 0.7f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
         LoadSettings();
         UpdateSlider();
         ApplySettings();
    }

    void UpdateSlider()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = masterVolume;
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        }

        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        ApplySettings();
        SaveSettings();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        ApplySettings();
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        ApplySettings();
        SaveSettings();
    }

    void ApplySettings()
    {
        AudioListener.volume = masterVolume;

        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
    }
    void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    void LoadSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.7f);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.7f);
    }

    public void OpenSettings()
    {
        if (SettingPanel != null)
        {
            SettingPanel.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (SettingPanel != null)
        {
            SettingPanel.SetActive(false);
        }
    }

    public float GetMasterVolume() { return masterVolume; }
    public float GetMusicVolume() { return musicVolume; }
    public float GetSFXVolume() { return sfxVolume; }
}
