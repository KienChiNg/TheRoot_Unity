using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private ActiveArea activeArea;
    [SerializeField] private int amountEnemySpawn;
    [SerializeField] private float countDown;
    // private GameObject spawnEnemyArea;
    private UIEnemy uIEnemy;
    private EnemyManager[] amountEnemy;
    public ActiveArea ActiveArea { get => activeArea; set => activeArea = value; }
    public int AmountEnemySpawn { get => amountEnemySpawn; set => amountEnemySpawn = value; }
    public float CountDown { get => countDown; set => countDown = value; }

    void Start()
    {
        uIEnemy = GetComponentInParent<UIEnemy>();
        // spawnEnemyArea = GameObject.FindGameObjectWithTag("SpawnEnemyArea");
        SpawnAmountEnemy();
        Init();
    }
    void Init()
    {
    }

    void SpawnAmountEnemy()
    {
        if (gameObject.transform.GetChild(1).gameObject.activeSelf)
            for (int i = 0; i < amountEnemySpawn; i++)
            {
                GameObject objManager = Instantiate(uIEnemy.EnemyObjManager, activeArea.GetRandomPoint(), Quaternion.identity, transform.GetChild(0).transform);
                Instantiate(enemy, objManager.transform.position, Quaternion.identity, objManager.transform);
            }
    }
  
}
