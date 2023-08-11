using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private UIGame uIGame;
    private UIEnemy uIEnemy;
    private GameObject player;
    private AutoMove autoMove;
    private Billboard billBoard;
    private ItemManager itemGame;
    private ActiveArea activeArea;
    private EnemySpawn enemySpawn;
    private ManagerGame gameManager;
    private PlayerManager playerManager;
    private EnemyAttribute enemyAttribute;
    private PlayerAttribute playerAttribute;
    private CircleSetActive circleSetActive;
    private AmountInCircleActive amountInCircleActive;
    private bool isCatch;
    private bool isBite;
    private bool isCirclActivePlayer;
    private float speed;
    private float radius;
    private float curDis;
    private float curDis2;
    private float countDownBiteIt;
    private float displacementDist = 25f;

    public bool IsCatch { get => isCatch; set => isCatch = value; }
    public bool IsBite { get => isBite; set => isBite = value; }
    public bool IsCirclActivePlayer { get => isCirclActivePlayer; set => isCirclActivePlayer = value; }


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
        circleSetActive = player.GetComponent<CircleSetActive>();
        amountInCircleActive = transform.parent.parent.GetComponentInChildren<AmountInCircleActive>();
        activeArea = transform.parent.parent.GetComponentInChildren<ActiveArea>();
        itemGame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemManager>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerGame>();
        enemyAttribute = transform.parent.GetComponentInParent<EnemyAttribute>();
        enemySpawn = transform.parent.GetComponentInParent<EnemySpawn>();
        uIEnemy = GameObject.FindGameObjectWithTag("EnemyOverall").GetComponent<UIEnemy>();
        uIGame = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UIGame>();
        autoMove = GetComponentInChildren<AutoMove>();

        SpawnBillBoard();
        billBoard = autoMove.gameObject.GetComponentInChildren<Billboard>();
        Init();
    }
    public void Init()
    {
        isCatch = false;
        isBite = false;
        countDownBiteIt = 4;
        InitStateEnemy();
        // gameObject.AddComponent<AutoMove>();
    }
    private void LateUpdate()
    {
        if(!autoMove.gameObject.activeSelf || gameManager.PauseGame) return;
        GetCurDis();
        CheckSetActive();
        ChangeStateMoveOfEnemy();
        RegionalInspections();
        // KillEnemy();
    }
    private void SpawnBillBoard()
    {
        Instantiate(uIEnemy.Billboard, autoMove.transform.position, Quaternion.identity, autoMove.gameObject.transform);
    }
    private void GetCurDis()
    {
        curDis = Vector3.Distance(autoMove.transform.position, player.transform.position);
        curDis2 = Vector3.Distance(autoMove.transform.position, activeArea.transform.position);
    }
    private void CheckSetActive()
    {
        if (curDis2 < activeArea.Range1 && autoMove.gameObject.activeSelf)
        {
            // Debug.Log("a");
            amountInCircleActive.StateEnemyInArea[transform.GetSiblingIndex()] = false;
        }
        else
        {
            // Debug.Log("b");
            amountInCircleActive.StateEnemyInArea[transform.GetSiblingIndex()] = true;
        }
    }
    void InitStateEnemy()
    {
        speed = enemyAttribute.Walk;
        radius = 0.0001f;
    }
    void SetStateEnemy(float speedE, float radiusE)
    {
        autoMove.Agent.speed = speedE;
        autoMove.Agent.radius = radiusE;
    }

    void RegionalInspections()
    {
        if (curDis <= playerAttribute.Circle.CircleRadius )
        {
            if (playerAttribute.MaxCapacity < playerManager.Capacity + enemyAttribute.Water)
            {
                if (!playerManager.StateTimeDelayVibrate)
                {
                    Handheld.Vibrate();
                    playerManager.StateTimeDelayVibrate = true;
                }
                return;
            }
            if (playerManager.AvailableRoot > 0 && !isCatch)
            {
                isCatch = true;
                billBoard.MinusHp = true;
                playerManager.DecreaseRoot();
                billBoard.UpdateStateBillboard(true);
                playerManager.HideActive = false;
            }
        }
        else
        {
            if (isCatch)
            {
                isCatch = false;
                playerManager.IncrementRoot();
                billBoard.UpdateStateBillboard(false);
            }
            billBoard.MinusHp = false;
        }
    }
    void ChangeStateMoveOfEnemy()
    {
        if (isCatch)
        {
            if (enemyAttribute.TypeEnemy == "Normal")
            {
                SetStateEnemy(enemyAttribute.Run, 0.25f);
                Vector3 normDir = (autoMove.transform.position - player.transform.position).normalized;
                // Debug.Log(normDir);
                autoMove.MoveToPos(player.transform.position + (normDir * displacementDist));
            }
            else if (enemyAttribute.TypeEnemy == "Stupid")
            {
                SetStateEnemy(enemyAttribute.Run, radius);
            }
            else if (enemyAttribute.TypeEnemy == "Attacker")
            {
                if (!isBite )
                {
                    autoMove.MoveToPos(player.transform.position);
                    SetStateEnemy(enemyAttribute.Run, 0.25f);
                    if (curDis <= 3)
                    {
                        isBite = true;
                        playerManager.SetStateHealth(enemyAttribute.Damage);
                    }
                }
                else
                {
                    SetStateEnemy(enemyAttribute.Walk / 3, radius);
                    countDownBiteIt -= Time.deltaTime;
                }
                if (countDownBiteIt <= 0)
                {
                    isBite = false;
                    countDownBiteIt = 4;
                }
            }
        }
        else
        {
            SetStateEnemy(speed, radius);
            countDownBiteIt = 4;
            isBite = false;
        }
    }

    public void KillEnemy()
    {
        IncrementItemValue();
        Init();
        playerManager.IncrementRoot();
        billBoard.UpdateStateBillboard(false);
        autoMove.gameObject.SetActive(false);
        StartCoroutine(WaitingToSpawnAgain());
    }

    IEnumerator WaitingToSpawnAgain()
    {
        yield return new WaitForSeconds(enemySpawn.CountDown);
        // autoMove.transform.position = activeArea.GetRandomPoint();
        autoMove.gameObject.SetActive(true);
    }

    void IncrementItemValue()
    {
        playerManager.IncrementWaterInCapacity(enemyAttribute.Water);
        playerManager.IncrementExp(enemyAttribute.Exp);
    }
}
