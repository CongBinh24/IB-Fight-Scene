using UnityEngine;

public class PunchEnemy : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy đã đấm trúng Player!");

            Player player = other.GetComponent<Player>();
            if (player != null && !player.isDead)
            {
                bool isCombo = Random.value > 0.5f;
                player.TakeDamage(damageAmount, isCombo);
            }
        }
    }
}
