using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveVisible : MonoBehaviour
{
    private EnemySpawn enemySpawn;
    private int amount;
    private void Start() {
        enemySpawn = transform.parent.GetComponent<EnemySpawn>();
        Init();
    }
    void Init(){
        amount = 0;
    }
    public void IncrementAmount()
    {
        amount ++;
    }
    public void DecreaseAmount()
    {
        amount --;
    }
}
