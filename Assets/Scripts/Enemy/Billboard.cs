using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Billboard : MonoBehaviour
{
    private GameObject cam;
    private GameObject enemyArea;
    private EnemyManager enemyManager;
    private ManagerGame gameManager;
    private EnemyAttribute enemyAttribute;
    private GameObject player;
    private PlayerAttribute playerAttribute;
    private Slider heathBar;
    private TMP_Text percentHeathTxt;
    private TMP_Text amountOfWaterTxt;

    private bool minusHp;
    public bool MinusHp { get => minusHp; set => minusHp = value; }

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerGame>();
        playerAttribute = player.GetComponent<PlayerAttribute>();

        enemyArea = gameObject.transform.parent.parent.gameObject;
        enemyManager = enemyArea.GetComponent<EnemyManager>();
        enemyAttribute = enemyArea.transform.parent.parent.GetComponent<EnemyAttribute>();

        percentHeathTxt = GetComponentInChildren<TMP_Text>();
        heathBar = GetComponentInChildren<Slider>();
        amountOfWaterTxt = transform.GetChild(2).GetComponentInChildren<TMP_Text>();

        Init();
        UpdateStateBillboard(false);
    }

    public void Init()
    {
        minusHp = false;
        heathBar.value = 1;
        PositionBillBoard(new Vector3(0, 5, 0));
    }
    private void FixedUpdate()
    {
        if(gameManager.PauseGame) return;
        SetPercentHeathTxt(Mathf.Round(heathBar.value * 100));
    }

    private void Update()
    {
        if(gameManager.PauseGame) return;
        UpdateHeathBar();
    }

    void LateUpdate()
    {
        if(gameManager.PauseGame) return;
        transform.LookAt(cam.transform.position + cam.transform.forward);
        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x + 90,
            0,
            transform.eulerAngles.z
        );
    }

    private void UpdateHeathBar()
    {
        if (minusHp)
        {
            heathBar.value -= playerAttribute.DamageForce / enemyAttribute.Heath * Time.deltaTime;
        }
        else
        {
            heathBar.value = 1;
        }
        if (heathBar.value <= 0)
        {
            enemyManager.KillEnemy();
        }
    }

    public void UpdateStateBillboard(bool state)
    {
        if (!state)
        {
            SetUp();
            Init();
        }
        gameObject.SetActive(state);
    }

    public void SetPercentHeathTxt(float percent)
    {
        percentHeathTxt.text = $"{percent.ToString()} %";
    }
    void SetUp()
    {
        SetAmountOfWaterTxt(enemyAttribute.Water);
        //.....//
    }
    public void SetAmountOfWaterTxt(int content)
    {
        amountOfWaterTxt.text = content.ToString();
    }

    public void PositionBillBoard(Vector3 plusPos)
    {
        transform.position = transform.parent.position + plusPos;
    }
}
