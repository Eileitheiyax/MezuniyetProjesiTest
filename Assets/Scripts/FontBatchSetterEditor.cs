#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEditor.SceneManagement;
using System.IO;

public class FontReplacerAllScenes : EditorWindow
{
    private TMP_FontAsset newFont;

    [MenuItem("Tools/Replace TMP Fonts In All Scenes")]
    public static void ShowWindow()
    {
        GetWindow<FontReplacerAllScenes>("Font Replacer (All Scenes)");
    }

    void OnGUI()
    {
        GUILayout.Label("Tüm sahnelerdeki TextMeshPro yazýlarýný deðiþtir", EditorStyles.boldLabel);
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("Yeni Font:", newFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Tüm sahnelerde uygula"))
        {
            if (newFont == null)
            {
                Debug.LogError("Lütfen bir TMP_FontAsset seç.");
                return;
            }

            ReplaceFontsInAllScenes();
        }
    }

    void ReplaceFontsInAllScenes()
    {
        string[] scenePaths = Directory.GetFiles("Assets/Scenes", "*.unity", SearchOption.AllDirectories);
        int totalTexts = 0;

        string currentScene = EditorSceneManager.GetActiveScene().path;

        foreach (string scenePath in scenePaths)
        {
            var scene = EditorSceneManager.OpenScene(scenePath);
            TMP_Text[] texts = GameObject.FindObjectsOfType<TMP_Text>(true);

            foreach (TMP_Text t in texts)
            {
                t.font = newFont;
                totalTexts++;
                EditorUtility.SetDirty(t); // deðiþiklik olduðunu iþaretle
            }

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log($"Sahne iþlendi: {scene.name} ({texts.Length} yazý)");
        }

        // Aktif sahneye geri dön
        if (!string.IsNullOrEmpty(currentScene))
        {
            EditorSceneManager.OpenScene(currentScene);
        }

        Debug.Log($" Tüm sahnelerde toplam {totalTexts} yazý güncellendi.");
    }
}
#endif
