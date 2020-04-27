using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Color = UnityEngine.Color;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public static Color[] colors = new Color[7];
    public int noOfBallsToSpawn = 30;

    private float _spawnRange = 14.0f;
    private float _spawnRangeY = 3.0f;
    private int _noOfBallsActive;
    private int _colorsInRainbow = 7;
    private int _minActiveTime = 30;
    private int _maxActiveTime = 70;
   
    private PlayerController playerScript;
    public Text _gameOverTxt;
    public ParticleSystem celebrateVictory;

    void Start()
    {
        //Fill array with rainbow color codes
        colors[0] = new Color(148f/255f, 0f, 211f/255f); //Violet
        colors[1] = new Color(75f/255f, 0f, 130f/255f); // Indigo
        colors[2] = new Color(0f, 0f, 255f/255f); //Blue
        colors[3] = new Color(0f, 255f/255f, 0f); //Green
        colors[4] = new Color(255f/255f, 255f/255f, 0f); //Yellow
        colors[5] = new Color(255f/255f, 127f/255f, 0f); //Orange
        colors[6] = new Color(255f/255f, 0f, 0f); //Red

        //Spawn balls with random color
        for (int i = 0; i < noOfBallsToSpawn; i++)
        {
            GameObject newBall = Instantiate(ballPrefab, GenerateSpawnPosition(), ballPrefab.transform.rotation);
            newBall.GetComponent<Renderer>().material.color = GetRandomColor();
            _noOfBallsActive++;
            StartCoroutine(BallCountDownRoutine(newBall));
        }

        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(_noOfBallsActive <= 0 && playerScript.noOfCollectedColors != _colorsInRainbow)
        {
            _gameOverTxt.text = "Game Over!";
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
           
        }
        else if(playerScript.noOfCollectedColors == _colorsInRainbow)
        {
            celebrateVictory.Play();
            _gameOverTxt.text = "You Won!";
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(0f, _spawnRange);
        float spawnPosY = Random.Range(0.0f, _spawnRangeY);
        float spawnPosZ = Random.Range(-_spawnRange, _spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 1f, spawnPosZ);

        return randomPos;
    }

    Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }

    IEnumerator BallCountDownRoutine(GameObject gb)
    {
        int randomTimeSec = Random.Range(_minActiveTime, _maxActiveTime);
        yield return new WaitForSeconds(randomTimeSec);
        _noOfBallsActive--;
        if(gb != null)
            gb.SetActive(false);
    }
}
