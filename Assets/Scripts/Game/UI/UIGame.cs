using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGame : MonoBehaviour
{
    [SerializeField] private TMP_Text coinTxt;
    [SerializeField] private TMP_Text waterTxt;
    [SerializeField] private TMP_Text capacityTxt;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private TMP_Text levelTxt;
    [SerializeField] private TMP_Text expTxt;
    [SerializeField] private Slider heathBar;
    [SerializeField] private Slider capacityBar;

    private void Start() {
        // SetLevel(Prefs.level);
    }
    public void SetCoinTxt(int content)
    {
        coinTxt.text = $"{content}";
    }
    public void SetWaterTxt(int content)
    {
        waterTxt.text = $"{content}";
    }

    public void SetCapacity(int maxCapacity, int curCapacity = 0)
    {
        capacityBar.value = (float)curCapacity/maxCapacity;
        capacityTxt.text = $"Capacity ({curCapacity}/{maxCapacity})";
    }

    public void SetHealth(int curHealth, int maxHealth)
    {
        heathBar.value = (float)curHealth/maxHealth;
        healthTxt.text = $"HP ({curHealth}/{maxHealth})";
    }
    public void SetLevel(int level){
        levelTxt.text = $"{level}";
    }
    public void SetExp(int curExp,int configExp){
        expTxt.text = $"{curExp}/{configExp}";
    }

}
