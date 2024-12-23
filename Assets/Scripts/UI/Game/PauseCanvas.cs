using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject doubleCheckPanel;
    [SerializeField] GameObject settingPanel;

    public Toggle fullScreenToggle;

    void Start()
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
    }


    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void OnEnable()
    {
        gameManager.isGamePaused = true;
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        settingPanel.SetActive(false);
        doubleCheckPanel.SetActive(false);
        pausePanel.SetActive(true);

        gameManager.isGamePaused = false;
        Time.timeScale = 1f;
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
        Screen.SetResolution(1920 * 2 / 3, 1080 * 2 / 3, false);
    }
}