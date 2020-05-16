using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        private int _currHealth;
        public int CurrentHealth
        {
            get { return _currHealth; }
            set { _currHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            _currHealth = maxHealth;
        }
    }

    EnemyStats enemyStats = new EnemyStats();

    [Header("Optional")]
    [SerializeField]
    private StatusIndicator statusIndicator;
   
    private void Start()
    {
        enemyStats.Init();
        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.CurrentHealth, enemyStats.maxHealth);
        }
    }
    public void DamageEnemy(int damageAmount)
    {
        enemyStats.CurrentHealth -= damageAmount;
        if (enemyStats.CurrentHealth <= 0)
        {
            Debug.Log("Kill Enemy!");
            GameMaster.KillEnemy(this);
        }

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(enemyStats.CurrentHealth, enemyStats.maxHealth);
        }
    }
}
