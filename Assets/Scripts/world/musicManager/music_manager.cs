using UnityEngine;

public class music_manager : MonoBehaviour
{
    private static music_manager instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesneyi sahne deðiþimlerinden koru
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // Eðer sahnede zaten bir MusicManager varsa, yenisini yok et
        }
    }
}

