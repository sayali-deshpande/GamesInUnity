using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int[] collectedColors = new int[7];
    public int noOfCollectedColors;
    public AudioClip collectBall;
    public AudioClip notAgainSameBall;
    public GameObject[] colorObjects;

    private AudioSource playerAudio;

    private GameObject colorObject;
   
    void Start()
    {
        for(int i = 0; i < 7; i++)
        {
            collectedColors[i] = -1;
            colorObjects[i].SetActive(false);
        }

        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Color getColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            int index = getColorIndex(getColor);
            if (collectedColors[index] == -1)
            {
                collectedColors[index] = 1;
                colorObjects[index].SetActive(true);
                noOfCollectedColors++;
                Destroy(other.gameObject);
                playerAudio.PlayOneShot(collectBall, 2.0f);
            }
            else
            {
                Debug.Log("You're trying to collect same color again!");
                playerAudio.PlayOneShot(notAgainSameBall, 2.0f);
            }
        }
    }

    private int getColorIndex(Color color)
    {
        int idx = 0;
        foreach(Color c in SpawnBall.colors)
        {
            if (c == color)
                return idx;
            idx++;
        }
        return -1;
    }
}
