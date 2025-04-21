using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance; // Singleton referans�

    public int maxHealth = 3;
    public int currentHealth;

    [Header("UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        // E�er daha �nce bir instance yoksa bu objeyi ata ve kal�c� yap
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesneyi sahne ge�i�lerinde silme
        }
        else
        {
            Destroy(gameObject); // Sahneye yanl��l�kla ikinci kez geldiyse yok et
            return;
        }
    }

    void Start()
    {
        // Sadece ilk sahnede can ba�lat�l�r
        if (currentHealth == 0)
            currentHealth = maxHealth;

        UpdateHearts();
    }

    public void LoseHealth()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Hasar al�nd�. Yeni can: " + currentHealth);
        UpdateHearts();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Can yenilendi. Yeni can: " + currentHealth);
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
