using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuilding : MonoBehaviour
{
    private GameObject target;
    private GameObject player;
    private Transform circle;
    private TargetIndicator targetIndicator;
    private ItemManager itemManager;
    private PlayerManager playerManager;
    private PlayerAttribute playerAttribute;

    private float hideDis = 13;
    private float vectorDisCur;
    // private float circleHomeBuilding;
    // public float CircleHomeBuilding { get => circleHomeBuilding; set => circleHomeBuilding = value; }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
        circle = transform.GetChild(0);
        itemManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ItemManager>();
        target = GameObject.FindGameObjectWithTag("TargetIndicator");
        targetIndicator = target.GetComponent<TargetIndicator>();
        Init();
    }

    private void Update()
    {
        vectorDisCur = (transform.position - target.transform.position).magnitude / 2;
        targetIndicator.SetDisTxt(Mathf.Round(vectorDisCur));
        if (vectorDisCur < hideDis)
        {
            OpenBank();
            target.SetActive(false);
        }
        else
            target.SetActive(true);
    }

    void Init()
    {
        // circle.gameObject.SetActive(false);
        // circle.gameObject.SetActive(true);
        // circleHomeBuilding = circle.CircleRadius;
        StartCoroutine(RotationCircle());
    }
    public void OpenBank()
    {
        // float dis = (transform.position - player.transform.position).magnitude;
        // if (dis <=  circleHomeBuilding + playerAttribute.Circle.CircleRadius)
        // {
        itemManager.IncrementCoin(playerManager.Capacity);
        playerManager.IncrementWaterInCapacity(-1);
        playerManager.HideActive = false;
        // circle.gameObject.SetActive(true);
        // }
        // else
        // {
        //     circle.gameObject.SetActive(false);
        // }
    }
    IEnumerator RotationCircle()
    {
        float time = 0;
        while (time < 1)
        {
            circle.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, 180, 0), time);
            time += 0.05f * Time.deltaTime;
            if (time >= 1) time = 0;
            yield return null;
        }
    }
}
