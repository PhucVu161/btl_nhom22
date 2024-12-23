using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    [SerializeField] public int maxHp = 1000;
    [SerializeField] public int hp = 1000;
    [SerializeField] Text hpText;

    private void Start()
    {
        hpText = GetComponentInChildren<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            hp -= enemy.atkToCrytal;
            Destroy(collision.gameObject);
            hpText.text = $"{hp}/{maxHp}";
        }

        if (hp <= 0)
        {
            // break animation
            GameManager.instance.GameOver();
        }
    }
}
