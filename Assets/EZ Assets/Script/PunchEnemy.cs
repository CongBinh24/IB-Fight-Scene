using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEnemy : MonoBehaviour
{
    public enum Hand { Left, Right };
    public int damgeAmout = 10;
    private void Start()
    {
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(Const.PLAYER_TAG))
        {
            Debug.Log("Da bi Enemy dam");
            // mất máu Player
            Player player = col.GetComponent<Player>();
            HealthBar healthBar = col.GetComponent<HealthBar>();
            if (player != null && !player.isDead)
            {
                bool isCombo = Random.value > 0.5f;
                player.TakeDamage(damgeAmout, isCombo);

                if (healthBar != null)
                {
                    healthBar.UpDateHealth(player.currentHealth);
                }
            }
        }
    }
}
