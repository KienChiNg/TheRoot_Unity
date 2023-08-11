using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemManager : MonoBehaviour
{
    private Item item;
    private UIGame uIGame;

    private void Start() {
        item = GetComponent<Item>();
        uIGame = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIGame>();
        Init();
    }
    void Init(){
        IncrementCoin(0);
    }
    public void IncrementCoin(int plus){
        item.Coin += plus;
        Prefs.coin = item.Coin;
        uIGame.SetWaterTxt(item.Coin);
    }
}
