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
        GUILayout.Label("T�m sahnelerdeki TextMeshPro yaz�lar�n� de�i�tir", EditorStyles.boldLabel);
        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("Yeni Font:", newFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("T�m sahnelerde uygula"))
        {
            if (newFont == null)
            {
                Debug.LogError("L�tfen bir TMP_FontAsset se�.");
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
                EditorUtility.SetDirty(t); // de�i�iklik oldu�unu i�aretle
            }

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log($"Sahne i�lendi: {scene.name} ({texts.Length} yaz�)");
        }

        // Aktif sahneye geri d�n
        if (!string.IsNullOrEmpty(currentScene))
        {
            EditorSceneManager.OpenScene(currentScene);
        }

        Debug.Log($" T�m sahnelerde toplam {totalTexts} yaz� g�ncellendi.");
    }
}
#endif
