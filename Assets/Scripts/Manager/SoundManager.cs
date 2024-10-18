using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBgm;
    public AudioSource audioSourceSfx;
    [Space(10f)]    
    public AudioClip paddleClip;
    public AudioClip wallClip;
    public AudioClip brickClip;

    // Update is called once per frame
    public void PlaySound()
    {
        if (audioSourceBgm != null)
        {
            audioSourceBgm.Play();
        }
    }

    public void PlaySfx(AudioClip clip)
    {
        audioSourceSfx.PlayOneShot(paddleClip);
    }
}
