using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AmountInCircleActive : MonoBehaviour
{
    private ManagerGame gameManager;
    private EnemySpawn enemySpawn;
    private ActiveArea activeArea;
    private GameObject player;
    private GameObject spawnEnemyArea;
    private CircleSetActive circleSetActive;
    private bool[] stateEnemyInArea;

    public bool[] StateEnemyInArea { get => stateEnemyInArea; set => stateEnemyInArea = value; }

    // private bool stateCircleActive;
    // public bool StateCircleActive { get => stateCircleActive; set => stateCircleActive = value; }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerGame>();
        enemySpawn = transform.GetComponentInParent<EnemySpawn>();
        activeArea = GetComponent<ActiveArea>();
        player = GameObject.FindGameObjectWithTag("Player");
        circleSetActive = player.GetComponent<CircleSetActive>();
        // spawnEnemyArea = GameObject.FindGameObjectWithTag("SpawnEnemyArea");
        spawnEnemyArea = transform.parent.GetChild(0).gameObject;
        Init();
    }
    void Init()
    {
        stateEnemyInArea = new bool[enemySpawn.AmountEnemySpawn];
        stateEnemyInArea = Enumerable.Repeat(false, enemySpawn.AmountEnemySpawn).ToArray();
        // amountEnemy = 0;
        // stateCircleActive = true;
    }
    private void Update()
    {
        // if(gameManager.PauseGame) return;
        SetActiveArea();
    }
    void SetActiveArea()
    {
        float dis = Vector3.Distance(transform.position, player.transform.position);
        if (dis <= activeArea.Range1 + circleSetActive.CircleWidthActive)
        {
            spawnEnemyArea.SetActive(true);
        }
        else
        {
            if (stateEnemyInArea.Contains(true))
            {
                // Debug.Log("aloo");
                spawnEnemyArea.SetActive(true);
            }
            else
            {
                // Debug.Log("alo2");
                spawnEnemyArea.SetActive(false);
            }
        }
    }
    // private void Update() {

    // }
    // public void IncrementAmount()
    // {
    //     amountEnemy ++;
    // }
    // public void DecreaseAmount()
    // {
    //     amountEnemy --;
    // }
}
