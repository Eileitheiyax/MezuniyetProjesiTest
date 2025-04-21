using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Sahne yönetimi için

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
            audioSource.PlayOneShot(buttonClickSound); // SES efekti

            if (!waitingForNextLevel)
            {
                storyText.text = currentEvent.result1;
                queuedNextLevel = currentEvent.nextLevelForChoice1;
                waitingForNextLevel = true;

                if (!currentEvent.isCorrectChoice1)
                    healthManager.LoseHealth();
                else if (currentEvent.healthChangeChoice1 > 0)
                    healthManager.Heal(currentEvent.healthChangeChoice1);

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
            audioSource.PlayOneShot(buttonClickSound); // SES efekti

            if (!waitingForNextLevel)
            {
                storyText.text = currentEvent.result2;
                queuedNextLevel = currentEvent.nextLevelForChoice2;
                waitingForNextLevel = true;

                if (!currentEvent.isCorrectChoice2)
                    healthManager.LoseHealth();
                else if (currentEvent.healthChangeChoice2 > 0)
                    healthManager.Heal(currentEvent.healthChangeChoice2);

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
        string sceneName = "Level" + (currentLevel + 1);
        SceneManager.LoadScene(sceneName);
    }
}
