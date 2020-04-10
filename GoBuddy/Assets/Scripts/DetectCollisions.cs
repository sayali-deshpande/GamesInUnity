using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public GameObject fireWork;
    public Text gameOverTxt;
    private void OnTriggerEnter(Collider other)
    {
        Explode();
        //Destroy both player and cube
        Destroy(this.gameObject);
        Destroy(other.gameObject);
        Debug.Log("game over!");        
    }

    void Explode()
    {
        if (fireWork)
        {
            GameObject particle = Instantiate(fireWork, this.gameObject.transform.position, Quaternion.identity);
            particle.GetComponent<ParticleSystem>().Play();

            gameOverTxt.text = "Game Over!";
        }
    }
}
