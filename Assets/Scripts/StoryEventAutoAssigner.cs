using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text.RegularExpressions;

public class StoryEventAutoAssigner : EditorWindow
{
    [MenuItem("Tools/Assign Story Events")]
    public static void AssignStoryEvents()
    {
        GameManager manager = FindObjectOfType<GameManager>();

        if (manager == null)
        {
            Debug.LogWarning("GameManager sahnede bulunamad�!");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:StoryEvent", new[] { "Assets/StoryEvents" });
        var storyEvents = guids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => AssetDatabase.LoadAssetAtPath<StoryEvent>(path))
            .OrderBy(s =>
            {
                // "Story12" gibi isimdeki 12'yi �ek
                var match = Regex.Match(s.name, @"\d+");
                return match.Success ? int.Parse(match.Value) : int.MaxValue;
            })
            .ToArray();

        if (storyEvents.Length == 0)
        {
            Debug.LogWarning("Hi� StoryEvent bulunamad�!");
            return;
        }

        manager.storyEvents = storyEvents;
        EditorUtility.SetDirty(manager);

        Debug.Log($"StoryEvents ba�ar�yla atand� ({storyEvents.Length} adet, numaraya g�re s�ral�).");
    }
}
