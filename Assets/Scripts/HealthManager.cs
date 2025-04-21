using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public Image[] hearts;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void LoseHealth()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < currentHealth);
        }
    }
}
