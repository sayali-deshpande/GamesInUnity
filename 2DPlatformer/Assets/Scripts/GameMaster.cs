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

    public CameraShake camShake;

    AudioSource audioData;
    private void Awake()
    {
        if (s_gm == null)
            s_gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();

    }

    private void Start()
    {
        audioData = GetComponent<AudioSource>();
        camShake = this.gameObject.GetComponent<CameraShake>();
        if(camShake == null)
        {
            Debug.LogError("Camera Shake is not referenced for Game Master script!");
        }
    }
    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        s_gm.StartCoroutine(s_gm.RespawnPlayer());
    }

    public static void KillEnemy(Enemy enemy)
    {
        s_gm._KillEnemy(enemy);
    }

    public IEnumerator RespawnPlayer()
    {
        audioData.Play();
        yield return new WaitForSeconds(spawnDelay);

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            Transform clone = Instantiate(spawnPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(clone.gameObject, 3f);
        }
    }

    public void _KillEnemy(Enemy _enemy)
    {
        Transform _clone = Instantiate(_enemy.enemyDeathParticles, _enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(_clone.gameObject, 5f);
        camShake.Shake(_enemy.camShakeAmt, _enemy.camShakeLength);
        Destroy(_enemy.gameObject);
    }
}
