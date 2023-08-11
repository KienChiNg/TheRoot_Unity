using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetIndicator : MonoBehaviour
{
    private Transform target;
    private Transform canvas;
    private TMP_Text distanceTxt;
    private MainBuilding mainBuilding;
    private float vectorDisCur;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Construction").transform.GetChild(0);
        mainBuilding = GameObject.FindGameObjectWithTag("Building").GetComponent<MainBuilding>();
        canvas = transform.GetChild(1);
        distanceTxt = canvas.GetComponentInChildren<TMP_Text>();
    }
    void Update()
    {
        transform.LookAt(target);
        canvas.transform.eulerAngles = new Vector3(
            transform.eulerAngles.x + 90,
            0,
            transform.eulerAngles.z
        );
    }
    public void SetChildrenActive(bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
        }
    }
    public void SetDisTxt(float content){
        distanceTxt.text = $"{content.ToString()} M";
    }
}
