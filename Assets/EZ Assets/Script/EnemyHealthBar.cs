using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image fillImage; // Image để fillAmount

    private float maxHealth;

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        fillImage.fillAmount = 1f;
    }

    public void UpdateHealth(float currentHealth)
    {
        fillImage.fillAmount = currentHealth / maxHealth;
    }
}
