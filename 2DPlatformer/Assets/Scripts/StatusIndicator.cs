using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text healthText;
    private Image healthBarImg;

    void Start()
    {
        if(healthBarRect == null)
        {
            Debug.LogError("ERROR- No health bar object referenced");
        }
        if (healthText == null)
        {
            Debug.LogError("ERROR - No health text object referenced");
        }

        healthBarImg = healthBarRect.GetComponent<Image>();
        if(healthBarImg == null)
        {
            Debug.LogError("Unable to reference HealthBar Image component");
        }
        healthBarImg.color = new Color(0, 255, 0);
    }

    public void SetHealth(int cur, int max)
    {
        float value = (float)cur / max;

        // change health bar color depending upon current health
        if (value <= 0.25)
            healthBarImg.color = Color.red; // red color
        else if (value > 0.25 && value < 0.50)
            healthBarImg.color = new Color(229, 207, 0); // yellow color

        healthBarRect.localScale = new Vector3(value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        healthText.text = cur + "/" + max + " HP";
    }
   
}
