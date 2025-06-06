using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public PlayerHealthBar healthBar;

    private void Start()
    {
        // Lưu chính xác vị trí ban đầu trong Scene
        currentHealth = maxHealth;
    }

    public void ResetPlayer()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.UpDateHealth(currentHealth);
        }

    }


}
