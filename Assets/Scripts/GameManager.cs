using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Choice
{
    public string choiceText;      // Seçenek metni
    public string resultText;      // Seçim sonucu
    public int nextEventID;        // Bir sonraki event ID'si
}

[System.Serializable]
public class GameEvent
{
    public int eventID;            // Olay kimliði
    public string storyText;       // Hikaye metni
    public Choice[] choices;       // Seçenekler
    public Sprite backgroundImage; // Arka plan görseli (JSON'da kullanmak istemezseniz çýkarabilirsiniz)
}

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public UnityEngine.UI.Image backgroundImage; // Arka plan görseli
    public TMPro.TextMeshProUGUI storyText;      // Hikaye metni
    public UnityEngine.UI.Button choice1Button;  // Seçenek 1 butonu
    public UnityEngine.UI.Button choice2Button;  // Seçenek 2 butonu
    public TMPro.TextMeshProUGUI choice1Text;    // Seçenek 1 yazý
    public TMPro.TextMeshProUGUI choice2Text;    // Seçenek 2 yazý

    private List<GameEvent> gameEvents;          // Oyun olaylarý
    private GameEvent currentEvent;              // Þu anki olay

    void Start()
    {
        LoadJSON();
        LoadEvent(1); // Ýlk event ID'si 1 ile baþlýyoruz
    }

    void LoadJSON()
    {
        // JSON'u Resources klasöründen yükle
        TextAsset jsonFile = Resources.Load<TextAsset>("storyevents");
        if (jsonFile != null)
        {
            gameEvents = new List<GameEvent>(JsonHelper.FromJson<GameEvent>(jsonFile.text));
            Debug.Log("JSON baþarýyla yüklendi!");
        }
        else
        {
            Debug.LogError("JSON dosyasý bulunamadý!");
        }
    }

    void LoadEvent(int eventID)
    {
        // Þu anki olayý bul ve UI'ye yükle
        currentEvent = gameEvents.Find(e => e.eventID == eventID);

        if (currentEvent != null)
        {
            storyText.text = currentEvent.storyText;

            // Seçenek 1
            choice1Text.text = currentEvent.choices[0].choiceText;
            choice1Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(() => OnChoiceSelected(0));

            // Seçenek 2
            choice2Text.text = currentEvent.choices[1].choiceText;
            choice2Button.onClick.RemoveAllListeners();
            choice2Button.onClick.AddListener(() => OnChoiceSelected(1));
        }
        else
        {
            Debug.LogError("Event ID " + eventID + " bulunamadý!");
        }
    }

    void OnChoiceSelected(int choiceIndex)
    {
        // Sonuç göster (geçici olarak hikaye metninin yerine yazdýrýyoruz)
        storyText.text = currentEvent.choices[choiceIndex].resultText;

        // Bir sonraki event'i yükle
        int nextEventID = currentEvent.choices[choiceIndex].nextEventID;
        if (nextEventID > 0)
        {
            Invoke("LoadNextEvent", 2f); // 2 saniye bekleyip bir sonraki event'i yükle
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

// JSON Helper Sýnýfý
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
