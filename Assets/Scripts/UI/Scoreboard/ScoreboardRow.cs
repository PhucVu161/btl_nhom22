using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardRow : MonoBehaviour
{
    public Text rankText;
    public Text waveText;
    public Text scoreText;
    public int score = -1;

    void Awake()
    {
        GetTextComponents();
        //rankText.text = "---";
        waveText.text = "---";
        scoreText.text = "---";
        score = -1;
    }
    void GetTextComponents()
    {
        Text[] texts = GetComponentsInChildren<Text>();

        foreach (Text text in texts)
        {
            if(text != null)
            {
                if (text.gameObject.name == "Rank")
                {
                    rankText = text;
                }
                else if (text.gameObject.name == "Wave")
                {
                    waveText = text;
                }
                else if (text.gameObject.name == "Score")
                {
                    scoreText = text;
                }
            }
            else
            {
                Debug.LogError("Text component is null in GetTextComponents.");
            }
        }
        
    }
    public void SetValue(int wave, int score)
    {
        if (wave > 0)
        {
            waveText.text = wave.ToString("N0");
            scoreText.text = score.ToString("N0");
            this.score = score;
        }
    }
}
