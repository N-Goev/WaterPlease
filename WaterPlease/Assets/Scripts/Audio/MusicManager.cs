using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip backgroundMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(backgroundMusic);
        }
    }
}
