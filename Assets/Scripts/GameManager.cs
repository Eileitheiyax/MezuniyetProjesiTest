using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI storyText;         // Hikaye metni
    public Button choice1Button;             // Se�enek 1 butonu
    public Button choice2Button;             // Se�enek 2 butonu
    public TextMeshProUGUI choice1Text;      // Se�enek 1 metni
    public TextMeshProUGUI choice2Text;      // Se�enek 2 metni

    [Header("Level Data")]
    public int currentLevel = 1;             // �u anki level

    [Header("Next Levels")]
    public string nextLevelForChoice1;       // Se�enek 1 i�in gidilecek level
    public string nextLevelForChoice2;       // Se�enek 2 i�in gidilecek level

    void Start()
    {
        // Ba�lang��ta ilk leveli y�kle
        LoadLevel(currentLevel);
    }

    void LoadLevel(int level)
    {
        currentLevel = level;

        // B�t�n level verilerini buraya gireceksin,
        switch (level)
        {
            case 1:
                // Level 1 bilgileri
                storyText.text = "You opened your eyes in the cradle.";
                choice1Text.text = "Cry";
                choice2Text.text = "Don�t Cry";
                nextLevelForChoice1 = "Level2";
                nextLevelForChoice2 = "Level3";

                // Butonlara i�lev ekle
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

                // Butonlara i�lev ekle
                SetButtonActions("The boxes tipped over and you fainted. -1 health",
                                 "Your father slapped you for making too much noise. -1 health");
                break;

            // Di�er level'lar buraya eklenebilir
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
        // Se�enek 1 i�lemleri
        choice1Button.onClick.RemoveAllListeners();
        choice1Button.onClick.AddListener(() =>
        {
            storyText.text = result1;
            choice1Text.text = "Next Level";
            choice1Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => LoadNextLevel(nextLevelForChoice1));
            choice2Button.interactable = false; // Di�er butonu devre d��� b�rak
        });

        // Se�enek 2 i�lemleri
        choice2Button.onClick.RemoveAllListeners();
        choice2Button.onClick.AddListener(() =>
        {
            storyText.text = result2;
            choice2Text.text = "Next Level";
            choice2Button.onClick.RemoveAllListeners();
            choice2Button.onClick.AddListener(() => LoadNextLevel(nextLevelForChoice2));
            choice1Button.interactable = false; // Di�er butonu devre d��� b�rak
        });
    }

    void LoadNextLevel(string nextLevelName)
    {
        // Belirlenen level'e ge�
        SceneManager.LoadScene(nextLevelName);
    }
}
