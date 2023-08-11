using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGame : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject shop;
    [SerializeField] private ItemUpgrades itemUpgrades;    
    private bool pauseGame;
    public bool PauseGame { get => pauseGame; set => pauseGame = value; }

    private void Start()
    {
        PauseGame = false;
        Application.targetFrameRate = 60;
    }
    // [SerializeField] private Item item;

    void Save()
    {
        player.GetComponent<PlayerAttribute>().SaveValue();
        player.GetComponent<PlayerManager>().SaveValue();
        GetComponent<Item>().SaveValue();
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            Save();
    }
    void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
            Save();
    }
    private void OnApplicationQuit()                     
    {
        Save();
    }
    public void OpenShop()
    {
        shop.SetActive(true);
        itemUpgrades.CheckStateInteractable();
    }
    public void CloseShop()
    {
        shop.SetActive(false);
    }
}
