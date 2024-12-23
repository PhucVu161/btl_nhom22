using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveAlarmCanvas : MonoBehaviour
{
    [SerializeField] Text waveText;
    int waveNumber;
    void Start()
    {
        if (waveText == null)
            waveText = GetComponentInChildren<Text>();
    }

    void Update()
    {
        waveNumber = GameManager.instance.wave;
        waveText.text = $"WAVE " + waveNumber;
    }
}
