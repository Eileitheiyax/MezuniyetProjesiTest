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
    public int currentLevel;                 // Şu anki level (Inspector'dan ayarlanabilir)

    [Header("Next Levels")]
    public string nextLevelForChoice1;       // Seçenek 1 için gidilecek level (Inspector'dan ayarlanır)
    public string nextLevelForChoice2;       // Seçenek 2 için gidilecek level (Inspector'dan ayarlanır)

    private bool resultDisplayed = false;    // Sonuç gösterildi mi?

    void Start()
    {
        // Başlangıçta Inspector'dan ayarlanan leveli yükle
        LoadLevel();
    }

    void LoadLevel()
    {
        // Şu anki levelin bilgilerini ayarla.Bu kısımda yeni level eklediğinizde error alıyonuz. File - Build settings kısmına git
        // Eklemek istediğin level da olduğundan emin ol (hierarchy kısmına bak), sağ aşağıda add scene var ona bas.
        switch (currentLevel)
        {
            case 1:
                storyText.text = "You opened your eyes in the cradle.";
                choice1Text.text = "Cry";
                choice2Text.text = "Don’t Cry";
                SetButtonActions("Your mom rushes to your side and takes care of you.",
                                 "You sleep so quietly that your family forgets you exist.");
                break;

            case 2:
                storyText.text = "You see a shining golden clock on top of the cabinet.";
                choice1Text.text = "Try to reach it by stacking the boxes";
                choice2Text.text = "Cry and tell your parents that you want the object";
                SetButtonActions("The boxes tipped over and you fainted. -1 health",
                                 "Your father slapped you for making too much noise. -1 health");
                break;

            case 3:
                storyText.text = "Now that you're older, your father said he's taking you hunting.";
                choice1Text.text = "Tell him you agreed to hunt";
                choice2Text.text = "Tell him you want to read a book";
                SetButtonActions("You set off into the woods with your father.",
                                 "Your father said, 'Are you going to study and become a philosopher?', slapped you and forced you to go hunting.");
                break;

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
        // Seçenek 1 işlemleri
        choice1Button.onClick.RemoveAllListeners();
        choice1Button.onClick.AddListener(() =>
        {
            if (!resultDisplayed) // Eğer sonuç gösterilmediyse
            {
                choice1Text.text = result1;    // Sonucu kartta göster
                resultDisplayed = true;       // Sonuç gösterildi olarak işaretle
                choice2Button.interactable = false; // Diğer butonu devre dışı bırak
            }
            else
            {
                LoadNextLevel(nextLevelForChoice1); // İkinci tıklamada Inspector'dan gelen değerle level geçişi
            }
        });

        // Seçenek 2 işlemleri
        choice2Button.onClick.RemoveAllListeners();
        choice2Button.onClick.AddListener(() =>
        {
            if (!resultDisplayed) // Eğer sonuç gösterilmediyse
            {
                choice2Text.text = result2;    // Sonucu kartta göster
                resultDisplayed = true;       // Sonuç gösterildi olarak işaretle
                choice1Button.interactable = false; // Diğer butonu devre dışı bırak
            }
            else
            {
                LoadNextLevel(nextLevelForChoice2); // İkinci tıklamada Inspector'dan gelen değerle level geçişi
            }
        });
    }

    void LoadNextLevel(string nextLevelName)
    {
        // Sonuç gösterimi sıfırla ve bir sonraki level'e geç
        resultDisplayed = false;
        SceneManager.LoadScene(nextLevelName);
    }
}
