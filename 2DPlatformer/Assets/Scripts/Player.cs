using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int health = 100;
    }

    PlayerStats playerStats = new PlayerStats();
    public int fallBoundry = -20;

    private void Update()
    {
        if(transform.position.y <= fallBoundry)
        {
            DamagePlayer(999999);
        }
    }
    public void DamagePlayer(int damageAmount)
    {
        playerStats.health -= damageAmount;
        if(playerStats.health <= 0)
        {
            Debug.Log("Kill player!");
            GameMaster.KillPlayer(this);
        }
    }
   
}
