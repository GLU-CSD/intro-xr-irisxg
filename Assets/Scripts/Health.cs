using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Dit is nodig om Image te gebruiken

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthbarFill;

    void UpdateHealthBar()
    {
        healthbarFill.fillAmount = currentHealth / maxHealth;
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        // Controleer of de health op 0 is
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void Die()
    {
        // Vernietig dit object met een delay van 1 seconde (optioneel)
        Destroy(gameObject, 0.5f);
    }
}
