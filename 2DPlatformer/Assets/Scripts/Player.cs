using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int currHealth;
        public int CurrentHealth
        {
            get { return currHealth; }
            set { currHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            currHealth = maxHealth;
        }
    }

    PlayerStats playerStats = new PlayerStats();
    public int fallBoundry = -20;

    [SerializeField]
    private StatusIndicator statusIndicator;

    private void Start()
    {
        if(statusIndicator == null)
        {
            Debug.LogError("Status indicator for player is not referenced!");
        }
        else
        {
            playerStats.Init();
            statusIndicator.SetHealth(playerStats.CurrentHealth, playerStats.maxHealth);
        }
    }
    private void Update()
    {
        if(transform.position.y <= fallBoundry)
        {
            DamagePlayer(999999);
        }
    }
    public void DamagePlayer(int damageAmount)
    {
        playerStats.CurrentHealth -= damageAmount;
        if(playerStats.CurrentHealth <= 0)
        {
            Debug.Log("Kill player!");
            GameMaster.KillPlayer(this);
        }
        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(playerStats.CurrentHealth, playerStats.maxHealth);
        }
    }
   
}
