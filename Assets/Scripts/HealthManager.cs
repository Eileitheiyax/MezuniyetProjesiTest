using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int maxHealth = 3;
    public int currentHealth;

    [SerializeField] private Image damageOverlay;
    [SerializeField] private float overlayFadeDuration = 0.5f;

    [SerializeField] private AudioSource damageAudioSource;
    [SerializeField] private AudioClip damageSound;

    [SerializeField] private Image healOverlay;
    [SerializeField] private float healOverlayFadeDuration = 0.5f;
    [SerializeField] private AudioSource healAudioSource;
    [SerializeField] private AudioClip healSound;


    [Header("UI")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (currentHealth == 0)
            currentHealth = maxHealth;

        UpdateHearts();
    }

    public void LoseHealth()
    {
        currentHealth--;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Hasar alýndý. Yeni can: " + currentHealth);

        if (damageAudioSource != null && damageSound != null)
        {
            damageAudioSource.PlayOneShot(damageSound); // HASAR SESÝ
        }

        ShowDamageOverlay();
        UpdateHearts();
        AnimateDamagedHeart();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log("Can yenilendi. Yeni can: " + currentHealth);

        ShowHealOverlay(); //  Ekraný yeþil yap
        PlayHealSound();   //  Ýyileþme sesi çal
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

    public void ShowDamageOverlay()
    {
        if (damageOverlay == null) return;

        StopAllCoroutines();
        StartCoroutine(FadeDamage());
    }

    private IEnumerator FadeDamage()
    {
        damageOverlay.color = new Color(1, 0, 0, 0.5f);

        float elapsedTime = 0f;
        while (elapsedTime < overlayFadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(0.5f, 0f, elapsedTime / overlayFadeDuration);
            damageOverlay.color = new Color(1, 0, 0, alpha);

            yield return null;
        }
    }

    //  Hasar alan kalbe animasyon ekleyen fonksiyon
    private void AnimateDamagedHeart()
    {
        if (currentHealth >= 0 && currentHealth < hearts.Length)
        {
            Image heart = hearts[currentHealth]; // Kýrýlan kalp
            if (heart != null)
            {
                StartCoroutine(PunchScale(heart.rectTransform));
            }
        }
    }

    private IEnumerator PunchScale(RectTransform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 enlargedScale = originalScale * 1.3f;
        float duration = 0.1f;

        target.localScale = enlargedScale;
        yield return new WaitForSeconds(duration);
        target.localScale = originalScale;
    }
    public void ShowHealOverlay()
    {
        if (healOverlay == null) return;

        StopCoroutine(nameof(FadeHeal));
        StartCoroutine(FadeHeal());
    }

    private IEnumerator FadeHeal()
    {
        healOverlay.color = new Color(0, 1, 0, 0.5f);

        float elapsedTime = 0f;
        while (elapsedTime < healOverlayFadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(0.5f, 0f, elapsedTime / healOverlayFadeDuration);
            healOverlay.color = new Color(0, 1, 0, alpha);

            yield return null;
        }
    }

    private void PlayHealSound()
    {
        if (healAudioSource != null && healSound != null)
        {
            healAudioSource.PlayOneShot(healSound);
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        damageOverlay = GameObject.Find("DamageOverlay")?.GetComponent<Image>();
        healOverlay = GameObject.Find("HealOverlay")?.GetComponent<Image>();
    }
}
