using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;

    //private int damgeAmout = 10;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamge(int damge)
    {
        currentHealth -= damge;

        if (currentHealth <= 0) 
        {
            Debug.Log("Die");
        }
    }
}
