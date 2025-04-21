using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryEvent", menuName = "Story Event")]
public class StoryEvent : ScriptableObject
{
    [TextArea(3, 10)]
    public string storyText;
    public string choice1Text;
    public string choice2Text;
    public string result1;
    public string result2;

    public int nextLevelForChoice1;
    public int nextLevelForChoice2;

    public bool isCorrectChoice1;
    public bool isCorrectChoice2;

    public int healthChangeChoice1; // +1 = can ekler
    public int healthChangeChoice2;
}
