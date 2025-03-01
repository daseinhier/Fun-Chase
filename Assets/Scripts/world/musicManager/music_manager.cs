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
            DontDestroyOnLoad(gameObject); // Bu nesneyi sahne de�i�imlerinden koru
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // E�er sahnede zaten bir MusicManager varsa, yenisini yok et
        }
    }
}

