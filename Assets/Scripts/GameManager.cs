using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Choice
{
    public string choiceText;      // Se�enek metni
    public string resultText;      // Se�im sonucu
    public int nextEventID;        // Bir sonraki event ID'si
}

[System.Serializable]
public class GameEvent
{
    public int eventID;            // Olay kimli�i
    public string storyText;       // Hikaye metni
    public Choice[] choices;       // Se�enekler
    public Sprite backgroundImage; // Arka plan g�rseli (JSON'da kullanmak istemezseniz ��karabilirsiniz)
}

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public UnityEngine.UI.Image backgroundImage; // Arka plan g�rseli
    public TMPro.TextMeshProUGUI storyText;      // Hikaye metni
    public UnityEngine.UI.Button choice1Button;  // Se�enek 1 butonu
    public UnityEngine.UI.Button choice2Button;  // Se�enek 2 butonu
    public TMPro.TextMeshProUGUI choice1Text;    // Se�enek 1 yaz�
    public TMPro.TextMeshProUGUI choice2Text;    // Se�enek 2 yaz�

    private List<GameEvent> gameEvents;          // Oyun olaylar�
    private GameEvent currentEvent;              // �u anki olay

    void Start()
    {
        LoadJSON();
        LoadEvent(1); // �lk event ID'si 1 ile ba�l�yoruz
    }

    void LoadJSON()
    {
        // JSON'u Resources klas�r�nden y�kle
        TextAsset jsonFile = Resources.Load<TextAsset>("storyevents");
        if (jsonFile != null)
        {
            gameEvents = new List<GameEvent>(JsonHelper.FromJson<GameEvent>(jsonFile.text));
            Debug.Log("JSON ba�ar�yla y�klendi!");
        }
        else
        {
            Debug.LogError("JSON dosyas� bulunamad�!");
        }
    }

    void LoadEvent(int eventID)
    {
        // �u anki olay� bul ve UI'ye y�kle
        currentEvent = gameEvents.Find(e => e.eventID == eventID);

        if (currentEvent != null)
        {
            storyText.text = currentEvent.storyText;

            // Se�enek 1
            choice1Text.text = currentEvent.choices[0].choiceText;
            choice1Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => OnChoiceSelected(0));

            // Se�enek 2
            choice2Text.text = currentEvent.choices[1].choiceText;
            choice2Button.onClick.RemoveAllListeners();
            choice2Button.onClick.AddListener(() => OnChoiceSelected(1));
        }
        else
        {
            Debug.LogError("Event ID " + eventID + " bulunamad�!");
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        // Sonu� g�ster (ge�ici olarak hikaye metninin yerine yazd�r�yoruz)
        storyText.text = currentEvent.choices[choiceIndex].resultText;

        // Bir sonraki event'i y�kle
        int nextEventID = currentEvent.choices[choiceIndex].nextEventID;
        if (nextEventID > 0)
        {
            Invoke("LoadNextEvent", 2f); // 2 saniye bekleyip bir sonraki event'i y�kle
        }
        else
        {
            Debug.Log("Oyun bitti!");
        }
    }

    void LoadNextEvent()
    {
        LoadEvent(currentEvent.choices[0].nextEventID);
    }
}

// JSON Helper S�n�f�
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
