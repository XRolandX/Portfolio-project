using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioClip sceneAmbientSound;

    private void Awake()
    {
        if (TryGetComponent<AudioSource>(out var audioSource))
        {
            audioSource.clip = sceneAmbientSound;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource component not founded" + gameObject.name);
        }
    }
}
