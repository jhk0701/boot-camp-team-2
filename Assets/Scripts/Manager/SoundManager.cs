using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SfxType
{
    PaddleHit = 0,
    WallHit,
    BrickHit,
    ItemUsage,
}

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBgm;
    public AudioSource audioSourceSfx;

    [Space(10f)]
    public AudioClip[] clips;

    void Start()
    {
        PlaySound();
    }

    // Update is called once per frame
    public void PlaySound()
    {
        audioSourceBgm.Play();
    }

    public void PlaySfx(SfxType type)
    {
        audioSourceSfx.PlayOneShot(clips[(int)type]);
    }
}
