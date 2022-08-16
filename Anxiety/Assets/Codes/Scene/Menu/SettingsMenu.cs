using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public Slider masterSlider = null;
    public Slider soundSlider = null;
    public Slider musicSlider = null;
    public AudioMixer audioMixer = null;
    string exposedParamName = null;
    void OnEnable()
    {
        masterSlider.minValue = soundSlider.minValue = musicSlider.minValue = -80f;
        masterSlider.maxValue = soundSlider.maxValue = musicSlider.maxValue = 20f;
        masterSlider.wholeNumbers = soundSlider.wholeNumbers = musicSlider.wholeNumbers = false;
    }
    public void SetMasterOnAudioMixer()
    {
        exposedParamName = "Master";
        audioMixer.SetFloat(exposedParamName, masterSlider.value);
    }
    public void SetSoundOnAudioMixer()
    {
        exposedParamName = "Sound";
        audioMixer.SetFloat(exposedParamName, soundSlider.value);
    }
    public void SetMusicOnAudioMixer()
    {
        exposedParamName = "Music";
        audioMixer.SetFloat(exposedParamName, musicSlider.value);
    }
}