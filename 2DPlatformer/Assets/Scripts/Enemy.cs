using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        public int damagePlayerAmt = 40;
      
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
    public Transform enemyDeathParticles;
    public float camShakeAmt = 0.1f;
    public float camShakeLength = 0.1f;

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
        if(enemyDeathParticles == null)
        {
            Debug.LogError("Enemy Death particle is not refernced!");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player _player = collision.gameObject.GetComponent<Player>();
        if(_player != null)
        {
            _player.DamagePlayer(enemyStats.damagePlayerAmt);
            DamageEnemy(9999);
        }
    }
}
