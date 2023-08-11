using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private int amountRoot;
    [SerializeField] private float damageForce;
    [SerializeField] private int maxCapacity;
    [SerializeField] private int health;
    private Circle circle;
    private int exp;
    private int level;

    public int AmountRoot { get => amountRoot; set => amountRoot = value; }
    public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
    public float PlayerSpeed { get => playerSpeed; set => playerSpeed = value; }
    public float DamageForce { get => damageForce; set => damageForce = value; }
    public int Health { get => health; set => health = value; }
    public Circle Circle { get => circle; set => circle = value; }
    public int Exp { get => exp; set => exp = value; }
    public int Level { get => level; set => level = value; }

    private void Awake() {
    }
    private void Start()
    {
        circle = GameObject.FindGameObjectWithTag("CirclePlayer").GetComponentInChildren<Circle>();
        Init();
        // ResetValue();
        // Debug.Log(circle.transform.localScale.x);
    }
    void Init()
    {
        playerSpeed = Prefs.playerSpeed > 0 ? Prefs.playerSpeed : Config.playerSpeedInitial;
        amountRoot = Prefs.amountRoot > 0 ? Prefs.amountRoot : Config.amountRootInitial;
        damageForce = Prefs.damageForce > 0 ? Prefs.damageForce : Config.damageForceInitial;
        maxCapacity = Prefs.maxCapacity > 0 ? Prefs.maxCapacity : Config.maxCapacityInitial;
        health = Prefs.health > 0 ? Prefs.health : Config.healthInitial;
        level = Prefs.level;
        exp = Prefs.exp;
        
        // circle.GetCircleRadius();
        // circle.UpdateCircleRadius();
    }
    public void SaveValue()
    {
        // Debug.Log(circle.CircleRadius);
        Prefs.playerSpeed = playerSpeed;
        Prefs.amountRoot = amountRoot;
        Prefs.damageForce = damageForce;
        Prefs.maxCapacity = maxCapacity;
        Prefs.radiusCircle = circle.LevelCircle;
        Prefs.health = health;
        Prefs.exp = exp;
        Prefs.level = level;
        // PlayerPrefs.Save();
    }

    public void ResetValue()
    {
        Prefs.playerSpeed = Config.playerSpeedInitial;
        Prefs.amountRoot = Config.amountRootInitial;
        Prefs.damageForce = Config.damageForceInitial;
        Prefs.maxCapacity = Config.maxCapacityInitial;
        Prefs.health = Config.healthInitial;
        Prefs.radiusCircle = 1;
        Prefs.exp = 0;
        Prefs.level = 0;
    }

    // public float Radius(){
    //     return circle.CircleRadius;
    // }
}
