using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Settings")]
    public Slider masterSlider = null;
    public Slider soundSlider = null;
    public Slider musicSlider = null;
    public AudioMixer audioMixer = null;
    string exposedParamName = null;
    [Header("Mouse Sounds")]
    public AudioSource mouseSoundPlayer = null;
    public AudioClip hoverSoundStorage = null;
    public AudioClip clickSoundStorage = null;
    void LoadAudio()
    {
        exposedParamName = "Master";
        audioMixer.SetFloat(exposedParamName, masterSlider.value = PlayerPrefs.GetFloat(exposedParamName));
        exposedParamName = "Sound";
        audioMixer.SetFloat(exposedParamName, soundSlider.value = PlayerPrefs.GetFloat(exposedParamName));
        exposedParamName = "Music";
        audioMixer.SetFloat(exposedParamName, musicSlider.value = PlayerPrefs.GetFloat(exposedParamName));
    }
    public void OnEnable()
    {
        masterSlider.minValue = soundSlider.minValue = musicSlider.minValue = -80f;
        masterSlider.maxValue = soundSlider.maxValue = musicSlider.maxValue = 20f;
        masterSlider.wholeNumbers = soundSlider.wholeNumbers = musicSlider.wholeNumbers = false;
        LoadAudio();
    }
    public void SetMasterOnAudioMixer()
    {
        exposedParamName = "Master";
        audioMixer.SetFloat(exposedParamName, masterSlider.value);
        PlayerPrefs.SetFloat(exposedParamName, masterSlider.value);
    }
    public void SetSoundOnAudioMixer()
    {
        exposedParamName = "Sound";
        audioMixer.SetFloat(exposedParamName, soundSlider.value);
        PlayerPrefs.SetFloat(exposedParamName, soundSlider.value);
    }
    public void SetMusicOnAudioMixer()
    {
        exposedParamName = "Music";
        audioMixer.SetFloat(exposedParamName, musicSlider.value);
        PlayerPrefs.SetFloat(exposedParamName, musicSlider.value);
    }
    public void SetMouseHoverSound()
    {
        mouseSoundPlayer.PlayOneShot(hoverSoundStorage);
    }
    public void SetMouseClickSound()
    {
        mouseSoundPlayer.PlayOneShot(clickSoundStorage);
    }
}