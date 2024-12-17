using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : MonoBehaviour
{
    [System.Serializable]
    public class Choice
    {
        public string choiceText;      // Seçenek metni
        public Sprite choiceImage;     // Seçenek görseli
        public string resultText;      // Seçim sonucu
        public int nextEventID;        // Bir sonraki event ID'si
    }

    [System.Serializable]
    public class GameEvent
    {
        public int eventID;            // Olay kimliði
        public string storyText;       // Hikaye metni
        public Choice[] choices;       // Seçenekler dizisi (2 seçim)
        public Sprite backgroundImage; // Arka plan görseli
    }

}
