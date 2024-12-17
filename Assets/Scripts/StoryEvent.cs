using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : MonoBehaviour
{
    [System.Serializable]
    public class Choice
    {
        public string choiceText;      // Se�enek metni
        public Sprite choiceImage;     // Se�enek g�rseli
        public string resultText;      // Se�im sonucu
        public int nextEventID;        // Bir sonraki event ID'si
    }

    [System.Serializable]
    public class GameEvent
    {
        public int eventID;            // Olay kimli�i
        public string storyText;       // Hikaye metni
        public Choice[] choices;       // Se�enekler dizisi (2 se�im)
        public Sprite backgroundImage; // Arka plan g�rseli
    }

}
