using UnityEngine;

public class Punch : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player đã đấm trúng Enemy!");

            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null && !enemy.isDead)
            {
                bool isCombo = Random.value > 0.5f;
                enemy.TakeDamage(damageAmount, isCombo);
            }
        }
    }
}
