using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private int maxHealth = 100;
    public Image healthFillBar;
    private int currentHealth;

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        UpDateHealth(currentHealth);
    }

    public void UpDateHealth(int currentHealth)
    {
        if (healthFillBar != null)
        {
            healthFillBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

   
}
