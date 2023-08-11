using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Transform circle;
    private PlayerAttribute playerAttribute;
    private Gate gate;
    private ManagerGame gameManager;
    private GameObject[] enemy;
    private GameObject[] building;
    private UIGame uIGame;
    private int availableRoot;
    private int capacity;
    private int heathLost;
    private int curHealth;
    private float timeDelayVibrate;
    private bool hideActive;
    private bool stateTimeDelayVibrate;

    public int AvailableRoot { get => availableRoot; set => availableRoot = value; }
    public int Capacity { get => capacity; set => capacity = value; }
    public bool HideActive { get => hideActive; set => hideActive = value; }
    public bool StateTimeDelayVibrate { get => stateTimeDelayVibrate; set => stateTimeDelayVibrate = value; }


    private void Start()
    {
        playerAttribute = GetComponent<PlayerAttribute>();
        gate = GameObject.FindGameObjectWithTag("Gate").GetComponent<Gate>();
        uIGame = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIGame>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerGame>();
        circle = gameObject.transform.parent.GetChild(1);
        Init();
    }
    public void Init()
    {
        int level = 0;

        while (level < Prefs.level)
        {
            gate.OpenGate(level, false);
            level++;
        }
        transform.localPosition = Prefs.GetPosition();
        // transform.localPosition = new Vector3(0,-1.12f,0);
        // capacity = Prefs.curCapacity;
        heathLost = 0;
        capacity = 0;
        SetStateHealth(-2);
        IncrementExp(0);
        uIGame.SetCapacity(playerAttribute.MaxCapacity, capacity);
        hideActive = false;
        availableRoot = playerAttribute.AmountRoot;
        stateTimeDelayVibrate = false;
        timeDelayVibrate = 3;
    }
    public void SaveValue()
    {
        // Debug.Log(heathLost);
        Prefs.SetPosition(transform.localPosition);
        // Prefs.heathLost = heathLost;
        // Prefs.curCapacity = capacity;
    }
    bool CheckStateDie()
    {
        if (heathLost >= playerAttribute.Health)
        {
            transform.localPosition = new Vector3(0, -1.12f, 0);
            SetStateHealth(-1);
            IncrementWaterInCapacity(-1);
            return true;
        }
        return false;
    }
    public void SetStateHealth(int lost)
    {
        if (lost == -1)
        {
            uIGame.SetHealth(playerAttribute.Health, playerAttribute.Health);
            heathLost = 0;
        }
        else if (lost == -2)
        {
            uIGame.SetHealth(playerAttribute.Health - heathLost, playerAttribute.Health);
        }
        else
        {
            heathLost += lost;
            int curHealth = playerAttribute.Health - heathLost;
            if (CheckStateDie()) return;
            uIGame.SetHealth(curHealth, playerAttribute.Health);
        }
    }
    private void Update()
    {
        if (gameManager.PauseGame) return;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        building = GameObject.FindGameObjectsWithTag("Building");
        CheckVibrate();
        SetCirclePos();
        CheckGameObjInRange();
        SaveValue();
        // CheckStateDie();
    }
    private void SetCirclePos()
    {
        circle.position = transform.position;
    }

    private void CheckVibrate()
    {
        if (stateTimeDelayVibrate)
        {
            timeDelayVibrate -= Time.deltaTime;
            if (timeDelayVibrate <= 0)
            {
                timeDelayVibrate = 3;
                stateTimeDelayVibrate = false;
            }
        }
    }

    private void CheckGameObjInRange()
    {
        if (availableRoot != playerAttribute.AmountRoot)
            SetCircleActiveState(true);
        else
            SetCircleActiveState(false);
    }

    public void DecreaseRoot()
    {
        availableRoot--;
    }
    public void IncrementRoot()
    {
        availableRoot++;
    }
    public void IncrementWaterInCapacity(int plus)
    {
        if (plus == -1)
            capacity = 0;
        else if (plus > -1)
        {
            if (capacity < playerAttribute.MaxCapacity)
                capacity += plus;
            if (capacity > playerAttribute.MaxCapacity)
                capacity = playerAttribute.MaxCapacity;
        }
        else
        {
            //Trường hợp bỏ trống để đồng bộ sau khi nâng cấp
        }
        uIGame.SetCapacity(playerAttribute.MaxCapacity, capacity);
    }
    public void IncrementExp(int exp)
    {
        // Debug.Log(Config.LEVEL_UP_EXP[playerAttribute.Level-1]);
        playerAttribute.Exp += exp;
        // if (exp != -1)
        // {
            if (playerAttribute.Level < Config.LEVEL_UP_EXP.Length && playerAttribute.Exp >= Config.LEVEL_UP_EXP[playerAttribute.Level])
            {
                gate.OpenGate(playerAttribute.Level, true);
                playerAttribute.Level++;
            }
            if (playerAttribute.Level >= Config.LEVEL_UP_EXP.Length)
            {
                playerAttribute.Level = Config.LEVEL_UP_EXP.Length - 1;
                playerAttribute.Exp = Config.LEVEL_UP_EXP[playerAttribute.Level];
                // break;
            }
        // }
        uIGame.SetExp(playerAttribute.Exp, Config.LEVEL_UP_EXP[playerAttribute.Level]);
        uIGame.SetLevel(playerAttribute.Level);
    }
    public void SetCircleActiveState(bool state)
    {
        playerAttribute.Circle.GetComponent<SpriteRenderer>().enabled = state;
    }
}
