using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip clickSFX;
    public AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.PlayOneShot(clickSFX);
    }
}
