using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI storyText;         // Hikaye metni
    public Button choice1Button;             // Seçenek 1 butonu
    public Button choice2Button;             // Seçenek 2 butonu
    public TextMeshProUGUI choice1Text;      // Seçenek 1 metni
    public TextMeshProUGUI choice2Text;      // Seçenek 2 metni

    [Header("Level Data")]
    public int currentLevel = 1;             // Þu anki level

    [Header("Next Levels")]
    public string nextLevelForChoice1;       // Seçenek 1 için gidilecek level
    public string nextLevelForChoice2;       // Seçenek 2 için gidilecek level

    void Start()
    {
        // Baþlangýçta ilk leveli yükle
        LoadLevel(currentLevel);
    }

    void LoadLevel(int level)
    {
        currentLevel = level;

        // Bütün level verilerini buraya gireceksin,
        switch (level)
        {
            case 1:
                // Level 1 bilgileri
                storyText.text = "You opened your eyes in the cradle.";
                choice1Text.text = "Cry";
                choice2Text.text = "Don’t Cry";
                nextLevelForChoice1 = "Level2";
                nextLevelForChoice2 = "Level3";

                // Butonlara iþlev ekle
                SetButtonActions("Your mom rushes to your side and takes care of you.",
                                 "You sleep so quietly that your family forgets you exist.");
                break;

            case 2:
                // Level 2 bilgileri
                storyText.text = "You see a shining golden clock on top of the cabinet.";
                choice1Text.text = "Try to reach it by stacking the boxes";
                choice2Text.text = "Cry and tell your parents that you want the object";
                nextLevelForChoice1 = "Level3";
                nextLevelForChoice2 = "Level4";

                // Butonlara iþlev ekle
                SetButtonActions("The boxes tipped over and you fainted. -1 health",
                                 "Your father slapped you for making too much noise. -1 health");
                break;

            // Diðer level'lar buraya eklenebilir
            default:
                storyText.text = "The End. Thanks for playing!";
                choice1Text.text = "";
                choice2Text.text = "";
                choice1Button.gameObject.SetActive(false);
                choice2Button.gameObject.SetActive(false);
                break;
        }
    }

    void SetButtonActions(string result1, string result2)
    {
        // Seçenek 1 iþlemleri
        choice1Button.onClick.RemoveAllListeners();
        choice1Button.onClick.AddListener(() =>
        {
            storyText.text = result1;
            choice1Text.text = "Next Level";
            choice1Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => LoadNextLevel(nextLevelForChoice1));
            choice2Button.interactable = false; // Diðer butonu devre dýþý býrak
        });

        // Seçenek 2 iþlemleri
        choice2Button.onClick.RemoveAllListeners();
        choice2Button.onClick.AddListener(() =>
        {
            storyText.text = result2;
            choice2Text.text = "Next Level";
            choice2Button.onClick.RemoveAllListeners();
            choice2Button.onClick.AddListener(() => LoadNextLevel(nextLevelForChoice2));
            choice1Button.interactable = false; // Diðer butonu devre dýþý býrak
        });
    }

    void LoadNextLevel(string nextLevelName)
    {
        // Belirlenen level'e geç
        SceneManager.LoadScene(nextLevelName);
    }
}
