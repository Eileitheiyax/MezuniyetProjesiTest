using UnityEngine;

[CreateAssetMenu(fileName = "New Story Event", menuName = "Story/Story Event")]
public class StoryEvent : ScriptableObject
{
    [TextArea] public string storyText;
    public string choice1Text;
    public string choice2Text;
    [TextArea] public string result1;
    [TextArea] public string result2;
    public int nextLevelForChoice1;
    public int nextLevelForChoice2;

    public bool isCorrectChoice1;
    public bool isCorrectChoice2;
    public bool isCheckpoint;

}
