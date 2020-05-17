using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count; // no of enemies in each wave
        public float spawnRate;
    }

    public Wave[] waves;
    public Transform[] spawnPoint;
    public float timeBetweenWaves = 3f;
    public SpawnState spawnState = SpawnState.COUNTING;

    private float waveCountDown;
    private int nextWave = 0;
    private float searchCountDown = 1f;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;

        if (spawnPoint.Length == 0)
            Debug.LogError("SpawnPoint for enemies are not referenced.");
    }

    private void Update()
    {
        if(spawnState == SpawnState.WAITING)
        {
            if(!IsEnemyAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountDown <= 0)
        {
            if(spawnState != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    bool IsEnemyAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }
        return true;
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed!");
        waveCountDown = timeBetweenWaves;
        spawnState = SpawnState.COUNTING;

        if (nextWave + 1 > waves.Length - 1)
        {
            Debug.Log("All wave completed! Looping.");
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave : " + _wave.name);
        spawnState = SpawnState.SPAWNING;
        for(int i = 0; i <_wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate); //wait for seconds before spawning next enemy
        }

        spawnState = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy: " + _enemy.name);
        Transform _sp = spawnPoint[Random.Range(0, spawnPoint.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
