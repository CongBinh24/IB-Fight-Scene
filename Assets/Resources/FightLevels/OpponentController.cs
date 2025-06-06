using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public int health;
    public float speed;
    public int power;

    private PlayerHealthBar healthBar;
    public void SetData(int health, int speed, int power)
    {
        this.health = health;
        this.speed = speed;
        this.power = power; 
    }

    public void SetHealthBar(PlayerHealthBar hb)
    {
        healthBar = hb;
    }

    // Hàm giả lập bị sát thương
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        if (healthBar != null)
            healthBar.UpDateHealth(health);

        if (health == 0)
            Die();
    }

    private void Die()
    {
        // Xử lý chết, hủy đối tượng...
        //Destroy(gameObject);
    }
}
