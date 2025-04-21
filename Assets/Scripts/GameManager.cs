using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI storyText;
    public Button choice1Button;
    public Button choice2Button;
    public TextMeshProUGUI choice1Text;
    public TextMeshProUGUI choice2Text;

    [Header("Story Data")]
    public StoryEvent[] storyEvents;
    public int currentLevel = 0;

    [Header("Health System")]
    public HealthManager healthManager;

    private bool waitingForNextLevel = false;
    private int queuedNextLevel = -1;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    void Start()
    {
        // Singleton üzerinden HealthManager al
        if (healthManager == null && HealthManager.instance != null)
        {
            healthManager = HealthManager.instance;
        }

        // Sahnedeki yeni kalp görsellerini bul ve isimlerine göre sırala → bağla
        if (healthManager != null)
        {
            GameObject[] heartObjects = GameObject.FindGameObjectsWithTag("Heart");

            // Kalpleri isme göre sırala: Heart1, Heart2, Heart3
            System.Array.Sort(heartObjects, (a, b) => a.name.CompareTo(b.name));

            Image[] heartImages = new Image[heartObjects.Length];

            for (int i = 0; i < heartObjects.Length; i++)
            {
                heartImages[i] = heartObjects[i].GetComponent<Image>();
            }

            healthManager.hearts = heartImages;
            healthManager.UpdateHearts();
        }

        LoadLevel();
    }

    void LoadLevel()
    {
        choice1Button.gameObject.SetActive(true);
        choice2Button.gameObject.SetActive(true);
        choice1Text.text = "";
        choice2Text.text = "";

        if (currentLevel >= storyEvents.Length || storyEvents[currentLevel] == null)
        {
            Debug.LogWarning("Geçerli hikaye bulunamadı. currentLevel: " + currentLevel);
            return;
        }

        waitingForNextLevel = false;
        queuedNextLevel = -1;

        StoryEvent currentEvent = storyEvents[currentLevel];

        storyText.text = currentEvent.storyText;
        choice1Text.text = currentEvent.choice1Text;
        choice2Text.text = currentEvent.choice2Text;

        choice1Button.onClick.RemoveAllListeners();
        choice2Button.onClick.RemoveAllListeners();

        // Seçim 1
        choice1Button.onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(buttonClickSound);

            if (!waitingForNextLevel)
            {
                storyText.text = currentEvent.result1;
                queuedNextLevel = currentEvent.nextLevelForChoice1;
                waitingForNextLevel = true;

                if (currentEvent.isCorrectChoice1) // ✔️ Tikliyse = Yanlış = Hasar
                {
                    Debug.Log("Seçim 1 yanlış (işaretli). Hasar alındı.");
                    if (healthManager != null) healthManager.LoseHealth();
                }
                else if (currentEvent.healthChangeChoice1 > 0)
                {
                    Debug.Log("Seçim 1 doğru. Can kazanıldı: " + currentEvent.healthChangeChoice1);
                    if (healthManager != null) healthManager.Heal(currentEvent.healthChangeChoice1);
                }

                choice1Text.text = "Continue";
                choice2Button.gameObject.SetActive(false);
            }
            else
            {
                currentLevel = queuedNextLevel;
                LoadNextLevel();
            }
        });

        // Seçim 2
        choice2Button.onClick.AddListener(() =>
        {
            audioSource.PlayOneShot(buttonClickSound);

            if (!waitingForNextLevel)
            {
                storyText.text = currentEvent.result2;
                queuedNextLevel = currentEvent.nextLevelForChoice2;
                waitingForNextLevel = true;

                if (currentEvent.isCorrectChoice2) // ✔️ Tikliyse = Yanlış = Hasar
                {
                    Debug.Log("Seçim 2 yanlış (işaretli). Hasar alındı.");
                    if (healthManager != null) healthManager.LoseHealth();
                }
                else if (currentEvent.healthChangeChoice2 > 0)
                {
                    Debug.Log("Seçim 2 doğru. Can kazanıldı: " + currentEvent.healthChangeChoice2);
                    if (healthManager != null) healthManager.Heal(currentEvent.healthChangeChoice2);
                }

                choice2Text.text = "Continue";
                choice1Button.gameObject.SetActive(false);
            }
            else
            {
                currentLevel = queuedNextLevel;
                LoadNextLevel();
            }
        });
    }

    private void LoadNextLevel()
    {
        string sceneName = "Level" + currentLevel; // +1 kaldırıldı
        Debug.Log("Yeni sahneye geçiliyor: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
