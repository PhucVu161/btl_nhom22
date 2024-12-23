using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Player player;

    [Header("Health Bar")]
    public int hp;
    public int maxHp;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Breaktime Timer")]
    public Text breaktimeTime;

    [Header("Numbers")]
    public Text goldText;
    public Text scoreText;
    public Image magic1Cooldown;

    void Update()
    {
        HPBar();
        BreaktimeTimer();
        Score();
        Gold();
        Magic();
    }

    void HPBar()
    {
        hp = player.hp;
        if (hp > maxHp) hp = maxHp;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < hp) hearts[i].sprite = fullHeart;
            else hearts[i].sprite = emptyHeart;

            hearts[i].enabled = i < maxHp;
        }
    }
    void BreaktimeTimer()
    {
        breaktimeTime.text = ((int)(GameManager.instance.breaktimetimer + 1)).ToString();
    }
    void Score()
    {
        scoreText.text = GameManager.instance.score.ToString("N0");
    }
    void Gold()
    {
        goldText.text = GameManager.instance.gold.ToString("N0") + "G";
    }
    void Magic()
    {
        magic1Cooldown.fillAmount = 1 - (player.timeLastAttack / player.coolDown);
    }
}
