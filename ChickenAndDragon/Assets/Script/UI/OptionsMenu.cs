using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Slider musicSlider;
    public Slider soundSlider;

    Resolution[] resolutions;

    private void Start() {
        //resolution dropdown
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 2; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //full screen
        fullScreenToggle.isOn = Screen.fullScreen;
        float tempValue;
        bool hasResult = audioMixer.GetFloat("MusicVolume", out tempValue);
        musicSlider.value = hasResult ? tempValue : 0f;
        hasResult = audioMixer.GetFloat("SoundVolume", out tempValue);
        soundSlider.value = hasResult ? tempValue : 0f;
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume(float volume) {
        audioMixer.SetFloat("SoundVolume", volume);
    }

    public void SetFullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }

}
