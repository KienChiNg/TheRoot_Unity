using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int coin;
    public int Coin { get => coin; set => coin = value; }

    private void Start() {
        Init();
    }
    private void Init(){
        coin = Prefs.coin;
        // coin = 0;
    }
    public void SaveValue(){
        Prefs.coin = coin;
    }
}
