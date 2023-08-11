using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUpgrades : MonoBehaviour
{
    [SerializeField] private UpgradeItemData[] upgradeItemData;
    [SerializeField] private Transform item;
    [SerializeField] private PlayerAttribute playerAttribute;
    [SerializeField] private float playerSpeedIncrease = 1;
    [SerializeField] private int amountRootIncrease = 1;
    [SerializeField] private float damageForceIncrease = 5;
    [SerializeField] private int maxCapacityIncrease = 5;
    [SerializeField] private int healthIncrease = 2;
    // [SerializeField] private Vector3 radiusIncrease = new Vector3(0.5f, 0.5f, 1);
    private GameObject player;
    private GameObject itemUpgrandeArea;
    private PlayerManager playerManager;
    private GameObject gameManager;
    private Item itemInGame;
    private ItemManager itemManager;
    private Image itemImage;
    private TMP_Text itemName;
    private TMP_Text itemEffect;
    private TMP_Text itemLevel;
    private TMP_Text itemDetail1;
    private TMP_Text itemDetail2;
    private Text waterNeed;
    private Button btn;
    private Circle circlePlayer;
    private int level;
    private string detail1;
    private string detail2;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        itemInGame = gameManager.GetComponent<Item>();
        itemManager = gameManager.GetComponent<ItemManager>();
        circlePlayer = GameObject.FindGameObjectWithTag("CirclePlayer").GetComponentInChildren<Circle>();
        itemUpgrandeArea = GameObject.FindGameObjectWithTag("ItemUpgrades");
        itemImage = item.GetChild(0).GetComponent<Image>();
        itemLevel = item.GetChild(1).GetComponent<TMP_Text>();
        itemName = item.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        itemEffect = item.GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        itemDetail1 = item.GetChild(2).GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        itemDetail2 = item.GetChild(2).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        waterNeed = item.GetChild(3).GetChild(1).GetComponent<Text>();
        btn = item.GetChild(3).GetComponent<Button>();
        Init();

    }
    private void Init()
    {
        for (int i = 0; i < upgradeItemData.Length; i++)
        {
            // level = SetLevel(i);
            if (upgradeItemData[i].itemSprite != null)
                itemImage.sprite = upgradeItemData[i].itemSprite;
            SetTextValue(upgradeItemData[i].itemName, itemName);
            SetTextValue(upgradeItemData[i].itemEffect, itemEffect);
            SetItemEasyToChange(i, false);
            upgradeItemData[i].itemLevel = level + 1;
            SetTextValue($"Level {upgradeItemData[i].itemLevel}", itemLevel);
            SetTextValue($"{detail1} to", itemDetail1);
            SetTextValue(detail2, itemDetail2);
            waterNeed.text = ((upgradeItemData[i].itemLevel) * upgradeItemData[i].waterNeed).ToString();
            GameObject item2 = Instantiate(item.gameObject, item.transform.position, Quaternion.identity, gameObject.transform);
            btn = item2.transform.GetChild(3).GetComponent<Button>();
            if (((upgradeItemData[i].itemLevel) * upgradeItemData[i].waterNeed) > itemInGame.Coin)
                btn.interactable = false;
            btn.AddEventListener(item2, OnShopItemBtnClicked);
            btn.AddEventListener(i, OnShopItemBtnClicked2);
        }
    }
    private void OnShopItemBtnClicked(GameObject itemClone)
    {
        itemLevel = itemClone.transform.GetChild(1).GetComponent<TMP_Text>();
        itemDetail1 = itemClone.transform.GetChild(2).GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        itemDetail2 = itemClone.transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        waterNeed = itemClone.transform.GetChild(3).GetChild(1).GetComponent<Text>();
    }
    private void OnShopItemBtnClicked2(int i)
    {
        // CheckStateInteractable();
        itemInGame.Coin -= (upgradeItemData[i].itemLevel) * upgradeItemData[i].waterNeed;
        CheckStateInteractable();
        SetItemEasyToChange(i, true);
        SetTextValue($"Level {upgradeItemData[i].itemLevel}", itemLevel);
        upgradeItemData[i].itemLevel += 1;
        SetTextValue($"{detail1} to", itemDetail1);
        SetTextValue(detail2, itemDetail2);
        waterNeed.text = ((upgradeItemData[i].itemLevel) * upgradeItemData[i].waterNeed).ToString();
        DataSync();
        playerAttribute.SaveValue();
    }
    public void CheckStateInteractable()
    {
        for (int i = 0; i < upgradeItemData.Length; i++)
        {
            // Debug.Log((level + 1) * upgradeItemData[i].waterNeed+" " +itemInGame.Coin);
            btn = itemUpgrandeArea.transform.GetChild(i).GetChild(3).GetComponent<Button>();
            if ((upgradeItemData[i].itemLevel) * upgradeItemData[i].waterNeed > itemInGame.Coin)
                btn.interactable = false;
            else
                btn.interactable = true;
        }
    }
    private void DataSync()
    {
        playerManager.AvailableRoot = playerAttribute.AmountRoot;
        playerManager.SetStateHealth(-1);
        playerManager.IncrementWaterInCapacity(-2);
        itemManager.IncrementCoin(0);
    }
    private void SetTextValue(string content, TMP_Text itemTxt)
    {
        // Debug.Log("aloo");
        itemTxt.text = content;
    }
    private void SetItemEasyToChange(int i, bool isClick)
    {
        switch (i)
        {
            case 0:
                if (isClick)
                {
                    playerAttribute.PlayerSpeed += playerSpeedIncrease;
                    // break;
                }
                level = (int)((playerAttribute.PlayerSpeed - Config.playerSpeedInitial) / playerSpeedIncrease);
                detail1 = playerAttribute.PlayerSpeed.ToString();
                detail2 = (playerAttribute.PlayerSpeed + playerSpeedIncrease).ToString();
                // Debug.Log(level);
                break;
            case 1:
                if (isClick)
                {
                    playerAttribute.MaxCapacity += maxCapacityIncrease;
                    // break;
                }
                level = (int)((playerAttribute.MaxCapacity - Config.maxCapacityInitial) / maxCapacityIncrease);
                detail1 = playerAttribute.MaxCapacity.ToString();
                detail2 = (playerAttribute.MaxCapacity + maxCapacityIncrease).ToString();
                break;
            case 2:
                if (isClick)
                {
                    playerAttribute.AmountRoot += amountRootIncrease;
                    // break;
                }
                level = (int)((playerAttribute.AmountRoot - Config.amountRootInitial) / amountRootIncrease);
                detail1 = playerAttribute.AmountRoot.ToString();
                detail2 = (playerAttribute.AmountRoot + amountRootIncrease).ToString();
                break;
            case 3:
                if (isClick)
                {
                    circlePlayer.UpdateCircleRadius();
                    // break;
                }
                level = circlePlayer.LevelCircle - 1;
                detail1 = (level + 10).ToString();
                detail2 = (level + 11).ToString();
                break;
            case 4:
                if (isClick)
                {
                    playerAttribute.DamageForce += damageForceIncrease;
                    // break;
                }
                level = (int)((playerAttribute.DamageForce - Config.damageForceInitial) / damageForceIncrease);
                detail1 = playerAttribute.DamageForce.ToString();
                detail2 = (playerAttribute.DamageForce + damageForceIncrease).ToString();
                break;
            case 5:
                if (isClick)
                {
                    playerAttribute.Health += healthIncrease;
                    // break;
                }
                level = (int)((playerAttribute.Health - Config.healthInitial) / healthIncrease);
                detail1 = playerAttribute.Health.ToString();
                detail2 = (playerAttribute.Health + healthIncrease).ToString();
                break;
            default:
                // code block
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
