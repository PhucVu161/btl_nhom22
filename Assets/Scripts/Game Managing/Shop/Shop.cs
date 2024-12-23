using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton
        public static Shop instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    GameManager gameManager;
    ObjectPooler objectPooler;
    Player player;

    public List<ShopItem> shopItems;
    public float timeHold = 0f;
    public float buyTime = 1f;
    private int isBuyingIndex = -1;
    public List<KeyCode> keyCodes = new List<KeyCode>
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5
    };


    void Start()
    {
        gameManager = GameManager.instance;
        objectPooler = ObjectPooler.instance;
        player = Player.instance;
                
        isBuyingIndex = -1;
    }

    void Update()
    {
        for(int i = 0; i < shopItems.Count; i++)
        {
            Buy(keyCodes[i], shopItems[i], i);
        }
        //Buy(KeyCode.Alpha1, shopItems[0], 0);
        //Buy(KeyCode.Alpha2, shopItems[1], 1);
    }
    private void Buy(KeyCode key, ShopItem shopItem, int shopItemIndex)
    {
        if ((isBuyingIndex != -1 && isBuyingIndex != shopItemIndex) || gameManager.gold < shopItem.hero.gold) return;

        if (Input.GetKey(key))
        {
            isBuyingIndex = shopItemIndex;
            timeHold += Time.deltaTime;
            shopItem.setFillAmount(timeHold / buyTime);
            if (timeHold > buyTime)
            {
                objectPooler.SpawnFromPool(shopItem.hero.name, player.transform.position, Quaternion.identity);
                timeHold = 0;
                shopItem.setFillAmount(0);
            }
        }

        if (Input.GetKeyUp(key) || player.isMoving)
        {
            timeHold = 0;
            shopItem.setFillAmount(0);
            isBuyingIndex = -1;
        }
    }
}