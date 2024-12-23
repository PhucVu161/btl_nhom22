using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
        public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] Slider volumeSlider;
    public AudioMixer audioMixer;
    
    

    public float breakTime = 10;
    bool isGameOver = false;
    public bool isGamePaused = false;

    [Header("Canvas")]
    public float breaktimetimer;
    public FadeCanvas breaktimeCanvas;
    public FadeCanvas gameOverCanvas;
    public Text resultWaveText;
    public Text resultScoreText;
    public FadeCanvas waveAlarmCanvas;
    public GameObject pauseCanvas;

    [Header("Score management")]
    public int wave = 1;
    public int score = 0;
    public int gold = 0;

    void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        if (volume != 0)
        {
            SetVolume(volume);
            volumeSlider.value = volume;
        }
        AudioManager.instance.Stop("Menu Theme");
        AudioManager.instance.Play("Battle Theme");

        breaktimetimer = breakTime;
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("lastscore", score);
        PlayerPrefs.SetInt("lastwave", wave);
        resultWaveText.text = "Wave: " + wave;
        resultScoreText.text = "Score: " + score;

        Time.timeScale = 0f;
        gameOverCanvas.ShowFadeIn();
        isGameOver = true;
        StartCoroutine(LoadResultScene());
    }
    public IEnumerator LoadResultScene()
    {
        //loat time = 0f;

        //while (time < 2.5f)
        //{
        //    time += Time.unscaledDeltaTime;

        //    yield return null;
        //}
        //if (time > 2.5f) SceneManager.LoadScene(2);

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(2);
            }
            yield return null;
        }
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void TogglePauseGame()
    {
        pauseCanvas.SetActive(!isGamePaused);
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
    }
    public void IncreaseGold(int amount)
    {
        gold += amount;
    }
    public void DecreaseGold(int amount)
    {
        gold -= amount;
    }
    public void SetVolume(float value)
    {
        audioMixer.SetFloat("volume", Mathf.Log(value) * 20f);
        PlayerPrefs.SetFloat("Volume", value);
    }
    public List<GameObject> activeHeroes;
    [HideInInspector]
    public List<GameObject> activeEnemies;
}
