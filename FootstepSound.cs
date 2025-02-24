using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip[] footstepClips; // Array to store multiple footstep sounds

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Function to play footstep sound
    public void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // Select a random footstep sound
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
