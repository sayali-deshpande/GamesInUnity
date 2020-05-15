using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster s_gm;
    public Transform playerPrefab;
    public Transform spawnPoint;
    public Transform spawnPrefab; // for particle system on player respawn
    public float spawnDelay = 2;

    AudioSource audioData;
    private void Start()
    {
        if (s_gm == null)
            s_gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

        audioData = GetComponent<AudioSource>();
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        s_gm.StartCoroutine(s_gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    public IEnumerator RespawnPlayer()
    {
        audioData.Play();
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
        Debug.Log("ToDo: Add SpawnParticles");
    }
}
