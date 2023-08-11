using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyObjManager;
    [SerializeField] private Transform canvas;
    private Billboard billboard;
    public Billboard Billboard { get => billboard; set => billboard = value; }
    public GameObject EnemyObjManager { get => enemyObjManager; set => enemyObjManager = value; }

    void Start()
    {
        billboard = canvas.GetComponent<Billboard>();
    }

}
