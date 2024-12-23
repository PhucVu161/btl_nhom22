using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{    
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;

    public Toggle fullScreenToggle;

    private void Start()
    {
        fullScreenToggle.isOn = PlayerPrefs.GetInt("isFullscreen", 1) == 1;

        if (fullScreenToggle.isOn)
        {
            SetFullScreenMode();
        }
        else
        {
            SetWindowedMode();
        }
        fullScreenToggle.isOn = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;

        float volume = PlayerPrefs.GetFloat("Volume");
        if (volume != 0)
        {
            SetVolume(volume);
            volumeSlider.value = volume;
        }
        Time.timeScale = 1.0f;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Scoreboard()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", Mathf.Log(value) * 20f);
        PlayerPrefs.SetFloat("Volume", value);
    }
    public void OnFullScreenToggleChanged(bool isFullscreen)
    {
        if (isFullscreen)
        {
            SetFullScreenMode();
        }
        else
        {
            SetWindowedMode();
        }
        PlayerPrefs.SetInt("isFullscreen", isFullscreen ? 1 : 0);
    }
    void SetFullScreenMode()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
    void SetWindowedMode()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(1920*2/3, 1080*2/3, false);
    }
}
