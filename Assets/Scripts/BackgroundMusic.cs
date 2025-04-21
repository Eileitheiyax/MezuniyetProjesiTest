using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesne sahne geçiþlerinde yok olmasýn
        }
        else
        {
            Destroy(gameObject); // Ayný müzikten ikinci bir tane olmasýn
        }
    }
}
