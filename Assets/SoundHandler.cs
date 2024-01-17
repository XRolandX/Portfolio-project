using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioClip sceneAmbientSound;


    void Awake()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
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
