using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AutoMove : MonoBehaviour
{
    private GameObject player;
    private PlayerAttribute playerAttribute;
    private NavMeshAgent agent;
    private GameObject enemyArea;
    private EnemySpawn enemySpawn;
    private EnemyManager enemyManager;
    private EnemyAttribute enemyAttribute;
    private ActiveArea activeAreaInPlayer;
    private ManagerGame gameManager;
    public NavMeshAgent Agent { get => agent; set => agent = value; }

    private void OnDisable()
    {
        transform.position = enemySpawn.ActiveArea.GetRandomPoint();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAttribute = player.GetComponent<PlayerAttribute>();
        agent = GetComponent<NavMeshAgent>();
        enemyManager = transform.parent.GetComponent<EnemyManager>();
        enemyArea = transform.parent.parent.parent.gameObject;
        enemySpawn = enemyArea.GetComponent<EnemySpawn>();
        enemyAttribute = enemyArea.GetComponent<EnemyAttribute>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerGame>();
        activeAreaInPlayer = GameObject.FindGameObjectWithTag("CirclePlayer").GetComponent<ActiveArea>();
    }
    void LateUpdate()
    {
        if (gameManager.PauseGame)
        {
            agent.isStopped = true;
            return;
        }else{
            agent.isStopped = false;
        }
        activeAreaInPlayer.Range1 = playerAttribute.Circle.CircleRadius;
        if (!agent.hasPath)
        {
            agent.SetDestination(enemySpawn.ActiveArea.GetRandomPoint());
        }
        else if (!agent.hasPath && enemyManager.IsCatch && enemyAttribute.TypeEnemy == "Attacker" && enemyManager.IsBite)
        {
            agent.SetDestination(activeAreaInPlayer.GetRandomPoint());
        }
    }
    public void MoveToPos(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
