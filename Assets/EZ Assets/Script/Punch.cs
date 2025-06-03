using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public int damgeAmout = 10;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.ENEMY_TAG))
        {
            Debug.Log("Da bi dam");
            // mất máu enemy 
            Enemy enemy = col.GetComponent<Enemy>();
            HealthBar healthBar = col.GetComponent<HealthBar>();
            if (enemy != null)
            {
                enemy.TakeDamge(damgeAmout);

                if(healthBar != null)
                {
                    healthBar.UpDateHealth(enemy.currentHealth);
                }
            }
        }
    }
}
