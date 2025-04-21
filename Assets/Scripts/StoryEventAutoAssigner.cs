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
            Debug.LogWarning("GameManager sahnede bulunamadý!");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:StoryEvent", new[] { "Assets/StoryEvents" });
        var storyEvents = guids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(path => AssetDatabase.LoadAssetAtPath<StoryEvent>(path))
            .OrderBy(s =>
            {
                // "Story12" gibi isimdeki 12'yi çek
                var match = Regex.Match(s.name, @"\d+");
                return match.Success ? int.Parse(match.Value) : int.MaxValue;
            })
            .ToArray();

        if (storyEvents.Length == 0)
        {
            Debug.LogWarning("Hiç StoryEvent bulunamadý!");
            return;
        }

        manager.storyEvents = storyEvents;
        EditorUtility.SetDirty(manager);

        Debug.Log($"StoryEvents baþarýyla atandý ({storyEvents.Length} adet, numaraya göre sýralý).");
    }
}
