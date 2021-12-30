using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    static float maxEnemyHp = 100;
    float enemyHp;
    // Start is called before the first frame update
    void Start()
    {
        enemyHp = maxEnemyHp;
        //GameEvents.instance.onDamageTaken += TakeDamage;

    }

    void Update()
    {
        
    }
    
    public void TakeDamage(float dmg)
    {
        enemyHp -= dmg;
        Debug.Log(enemyHp);
        if(enemyHp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
