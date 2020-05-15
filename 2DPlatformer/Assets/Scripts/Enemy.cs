using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int health = 100;
    }

    EnemyStats enemyStats = new EnemyStats();
    public int fallBoundry = -20;

  
    public void DamageEnemy(int damageAmount)
    {
        enemyStats.health -= damageAmount;
        if (enemyStats.health <= 0)
        {
            Debug.Log("Kill Enemy!");
            GameMaster.KillEnemy(this);
        }
    }
}
