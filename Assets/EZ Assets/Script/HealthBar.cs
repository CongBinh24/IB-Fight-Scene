using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private int maxHealth = 100;
    private int currentHealth;

    public Image healthFillBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (currentHealth < 0)
        {
            currentHealth = 0;
            Debug.Log("Die");
        }

        UpDateHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpDateHealth(int currentHealth)
    {
        if (healthFillBar != null)
        {
            healthFillBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }
}
