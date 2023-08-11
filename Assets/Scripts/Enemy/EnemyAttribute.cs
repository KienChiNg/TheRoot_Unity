using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum Type { Spoon, Cup, Bowl, Piece }
public class EnemyAttribute : MonoBehaviour
{
    [SerializeField] private int water;
    [SerializeField] private float walk = 3.5f;
    [SerializeField] private float run;
    [SerializeField] private float heath;
    [SerializeField] private int exp;
    [SerializeField] private string typeEnemy = "Normal"; // "Attacker", "Stupid"
    // [DrawIf("someBool", true, ComparisonType.Equals, DisablingType.ReadOnly)]
    [SerializeField] private int damage;

    public int Water { get => water; set => water = value; }
    public float Run { get => run; set => run = value; }
    public float Heath { get => heath; set => heath = value; }
    public string TypeEnemy { get => typeEnemy; set => typeEnemy = value; }
    public float Walk { get => walk; set => walk = value; }
    public int Exp { get => exp; set => exp = value; }
    public int Damage { get => damage; set => damage = value; }
}
