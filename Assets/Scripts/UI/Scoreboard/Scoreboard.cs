using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoreboard : MonoBehaviour
{
    public List<ScoreboardRow> rowObjects;

    void Start()
    {
        Time.timeScale = 1.0f;

        #region test data
        //PlayerPrefs.DeleteAll();

        //PlayerPrefs.SetInt("waveScoreboard1", 1);
        //PlayerPrefs.SetInt("waveScoreboard2", 2);
        //PlayerPrefs.SetInt("waveScoreboard3", 3);
        //PlayerPrefs.SetInt("waveScoreboard4", 4);
        //PlayerPrefs.SetInt("waveScoreboard5", 5);

        //PlayerPrefs.SetInt("waveScoreboard6", 10);
        //PlayerPrefs.SetInt("waveScoreboard7", 9);
        //PlayerPrefs.SetInt("waveScoreboard8", 8);
        //PlayerPrefs.SetInt("waveScoreboard9", 7);
        //PlayerPrefs.SetInt("waveScoreboard10", 6);

        //PlayerPrefs.SetInt("scoreScoreboard1", 111111111);
        //PlayerPrefs.SetInt("scoreScoreboard2", 22222222);
        //PlayerPrefs.SetInt("scoreScoreboard3", 333333);
        //PlayerPrefs.SetInt("scoreScoreboard4", 44);
        //PlayerPrefs.SetInt("scoreScoreboard5", 55);

        //PlayerPrefs.SetInt("scoreScoreboard6", 100);
        //PlayerPrefs.SetInt("scoreScoreboard7", 99);
        //PlayerPrefs.SetInt("scoreScoreboard8", 88);
        //PlayerPrefs.SetInt("scoreScoreboard9", 77);
        //PlayerPrefs.SetInt("scoreScoreboard10", 66);

        //PlayerPrefs.SetInt("lastwave", 999);
        //PlayerPrefs.SetInt("lastscore", 214);
        #endregion

        // Get min index
        int minScoreIndex = 1;
        SetMinScorePlayerPrefs(ref minScoreIndex);

        // Set new min score
        SetNewMinScore(minScoreIndex);

        // Put data into rowObjects
        SetRowObjectsData();

        // Sort
        List<ScoreboardRow> temp = rowObjects;
        temp = SortRowObjects(temp, false);
        for (int i = 0; i < rowObjects.Count; i++)
        {
            rowObjects[i] = temp[i];
            rowObjects[i].transform.SetSiblingIndex(i);
        }

        //Ranking
        SetRankText();
    }
    void SetMinScorePlayerPrefs(ref int minScoreIndex)
    {
        for (int i = 2; i <= rowObjects.Count; i++)
        {
            if (PlayerPrefs.GetInt("scoreScoreboard" + minScoreIndex) > PlayerPrefs.GetInt("scoreScoreboard" + i))
            {
                minScoreIndex = i;
            }
        }
    }
    void SetNewMinScore(int minScoreIndex)
    {
        int lastWave = PlayerPrefs.GetInt("lastwave");
        int lastScore = PlayerPrefs.GetInt("lastscore");
        if (lastScore > PlayerPrefs.GetInt("scoreScoreboard" + minScoreIndex))
        {
            PlayerPrefs.SetInt("waveScoreboard" + minScoreIndex, lastWave);
            PlayerPrefs.SetInt("scoreScoreboard" + minScoreIndex, lastScore);
        }
    }
    void SetRowObjectsData()
    {
        for (int i = 1; i <= rowObjects.Count; i++)
        {
            if (rowObjects[i-1] != null)
            {
                rowObjects[i-1].SetValue(PlayerPrefs.GetInt("waveScoreboard" + i),
                PlayerPrefs.GetInt("scoreScoreboard" + i));
            }
        }
    }
    List<ScoreboardRow> SortRowObjects(List<ScoreboardRow> list, bool ascendingOrder)
    {
        list.Sort((a, b) => ascendingOrder ? a.score.CompareTo(b.score) : b.score.CompareTo(a.score));
        return list;
    }
    void SetRankText()
    {
        for (int i = 0; i < rowObjects.Count; i++)
        {
            if (rowObjects[i] != null)
            {
                if (rowObjects[i].rankText != null)
                {
                    switch (i)
                    {
                        case 0:
                            rowObjects[i].rankText.text = "1st";
                            break;
                        case 1:
                            rowObjects[i].rankText.text = "2nd";
                            break;
                        case 2:
                            rowObjects[i].rankText.text = "3rd";
                            break;
                        default:
                            rowObjects[i].rankText.text = $"{i + 1}th";
                            break;
                    }
                }
                else Debug.Log("rankText null" + i);
            }
            else Debug.Log("Obj null" + i);
        }

    }

    public void ReplayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}