using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] backgroundClips;

    private void Start()
    {
        audioSource.PlayOneShot(backgroundClips[Random.Range(0, 6)]);
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(backgroundClips[Random.Range(0, 6)]);
    }
}
