using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAudio : MonoBehaviour
{
    public AudioSource audioS;
    public void PlayClip(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
