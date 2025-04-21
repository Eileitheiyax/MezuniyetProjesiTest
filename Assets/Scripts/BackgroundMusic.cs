using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesne sahne ge�i�lerinde yok olmas�n
        }
        else
        {
            Destroy(gameObject); // Ayn� m�zikten ikinci bir tane olmas�n
        }
    }
}
