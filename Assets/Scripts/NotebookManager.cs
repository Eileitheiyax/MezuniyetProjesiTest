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
            DontDestroyOnLoad(gameObject); // Sahne ge�ince silinmesin
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
        // Referans� s�f�rla
        noteInputField = null;

        // Yeni sahnedeki NoteInputField'� bulmaya �al��
        GameObject foundObj = GameObject.Find("NoteInputField");

        if (foundObj != null)
        {
            TMP_InputField foundField = foundObj.GetComponent<TMP_InputField>();
            if (foundField != null)
            {
                noteInputField = foundField;

                // Paneli kapal� ba�latmak istiyorsan:
                noteInputField.transform.parent.gameObject.SetActive(false);

                // Text�i geri y�kle
                noteInputField.text = currentNote;

                // Dinleyici ba�lant�s� kur
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
