using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Hero hero;
    public Text priceText;
    public Image holdTimeImage;

    private void Start()
    {
        priceText.text = hero.gold.ToString() + "G";
        setFillAmount(0);
    }
    public void setFillAmount(float fillAmount)
    {
        holdTimeImage.fillAmount = fillAmount;
    }
}
