using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class NotebookManager : MonoBehaviour
{
    public static NotebookManager instance;

    public TMP_InputField noteInputField;
    private string currentNote = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne geçince silinmesin
        }
        else
        {
            Destroy(gameObject); // Zaten varsa yeni geleni sil
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Referansý sýfýrla
        noteInputField = null;

        // Yeni sahnedeki NoteInputField'ý bulmaya çalýþ
        GameObject foundObj = GameObject.Find("NoteInputField");

        if (foundObj != null)
        {
            TMP_InputField foundField = foundObj.GetComponent<TMP_InputField>();
            if (foundField != null)
            {
                noteInputField = foundField;

                // Paneli kapalý baþlatmak istiyorsan:
                noteInputField.transform.parent.gameObject.SetActive(false);

                // Text’i geri yükle
                noteInputField.text = currentNote;

                // Dinleyici baðlantýsý kur
                noteInputField.onValueChanged.AddListener(UpdateNote);
            }
        }
    }



    void Start()
    {
        noteInputField.transform.parent.gameObject.SetActive(false);

        if (noteInputField != null)
        {
            noteInputField.text = currentNote;
            noteInputField.onValueChanged.AddListener(UpdateNote);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (noteInputField != null)
            {
                GameObject panel = noteInputField.transform.parent.gameObject;
                panel.SetActive(!panel.activeSelf);
            }
        }
    }


    private void UpdateNote(string newText)
    {
        currentNote = newText;
    }
}
