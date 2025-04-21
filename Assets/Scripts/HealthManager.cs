using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance; // Singleton referansý

    public int maxHealth = 3;
    public int currentHealth;

    [Header("UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        // Eðer daha önce bir instance yoksa bu objeyi ata ve kalýcý yap
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesneyi sahne geçiþlerinde silme
        }
        else
        {
            Destroy(gameObject); // Sahneye yanlýþlýkla ikinci kez geldiyse yok et
            return;
        }
    }

    void Start()
    {
        // Sadece ilk sahnede can baþlatýlýr
        if (currentHealth == 0)
            currentHealth = maxHealth;

        UpdateHearts();
    }

    public void LoseHealth()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Hasar alýndý. Yeni can: " + currentHealth);
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
